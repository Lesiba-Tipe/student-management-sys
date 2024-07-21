using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using student_management_sys.Configs;
using student_management_sys.Dto;
using student_management_sys.Inputs;
using student_management_sys.Services;

namespace student_management_sys.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private IStudentService studentService;
        public StudentController(ILogger<AccountController> logger, IMapper mapper, StudManSysDBContext context) 
            : base(logger, mapper, context)
        {
            studentService = new StudentService(context);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] StudentInput studentInput)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetUser(string id)
        {
            if (context == null)
                return NotFound();

            var user = await studentService.FindStudentByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await studentService.GetRolesAsync(user);

            //Map Data
            var userDto = mapper.Map<StudentDto>(user);
            userDto.Roles = roles;

            return userDto;
        }
    }
}
