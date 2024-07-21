using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using student_management_sys.Configs;
using student_management_sys.Dto;
using student_management_sys.Entity;
using student_management_sys.Inputs;
using student_management_sys.Services;
using System.Configuration;

namespace student_management_sys.Controllers
{
    
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private IStudentService studentService;
        private AccountService accountService;
        private readonly CSVService csvService;
        public StudentController(
            ILogger<AccountController> logger, IMapper mapper, 
            StudManSysDBContext context, UserManager<Account> accountManager, IConfiguration configuration) 
            : base(logger, mapper, context)
        {
            studentService = new StudentService(context);
            accountService = new AccountService(accountManager, context,mapper);
            csvService = new CSVService();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] StudentInput studentInput)
        {
            logger.LogInformation($"Attemptting to create student for {studentInput.IDNumber}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var results = await studentService.CreateAsync(mapper.Map<Student>(studentInput));
                await studentService.SaveChangesAsync();

                return Accepted(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(Create)}: ");

                return StatusCode(500, $"Internal server Error");
            }
            
        }

        [Authorize(Roles = "Admin, Student, Parent")]
        [HttpGet("get-student/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(string id)
        {
            if (context == null)
                return NotFound();

            var student = await studentService.FindStudentByIdNumberAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);           
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-all")]
        public async Task<ActionResult<StudentDto>> GetStudents()
        {
            var students = await studentService.FindAllStudentsByAsync();

            return Ok(students);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(string id, [FromBody] StudentDto studentDto)
        {

            if (id != studentDto.Id)
                return BadRequest("Content did not match subject");

            try
            {
                await studentService.UpdateAsync(mapper.Map<Student>(studentDto));

                await studentService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntryExist(id))
                    return NotFound(ex);
                else
                    throw;
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<StudentDto>> DeleteStudent(string id)
        {

            try
            {
                await studentService.DeleteAsync(id);
                await studentService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntryExist(id))
                    return NotFound(ex);
                else
                    throw;
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("import")]    //Upload csv then return data in json
        public async Task<IActionResult> ImportContactsToCSV([FromForm] IFormFileCollection file)
        {
            logger.LogInformation($"Attemptting to upload csv and saving to database");

            try
            {
                var students = await csvService.GetRecordsAsync(file[0].OpenReadStream());

                //Add to Database
                await context.Students.AddRangeAsync(mapper.Map<Student>(students));
                context.SaveChanges();

                return Ok(students);
            }
                catch(Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(ImportContactsToCSV)}: ");
                return StatusCode(500, $"Internal server Error");
            }
        }

        private bool EntryExist(string email)
        {
            return (context.Users?.Any(user => user.Email == email)).GetValueOrDefault();
        }
    }
}
