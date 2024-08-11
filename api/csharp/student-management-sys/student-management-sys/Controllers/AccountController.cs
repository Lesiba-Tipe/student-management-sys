using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_management_sys.Configs;
using student_management_sys.Dto;
using student_management_sys.Entity;
using student_management_sys.Inputs;
using student_management_sys.Services;


namespace student_management_sys.Controllers
{
    //[EnableCors("MyAllowSpecificOrigins")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAuthService authService;
        private readonly AccountService accountService;

        public AccountController(
            ILogger<AccountController> logger, IMapper mapper, IAuthService authService, 
            UserManager<Account> accountManager,StudManSysDBContext context) : base(logger, mapper, context)
        {
            this.authService = authService;
            accountService = new AccountService(accountManager, context);
        }
        // LOGIN | REGISTER

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterInput registerInput)
        {
            logger.LogInformation($"Registration attempt for {registerInput.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var results = await accountService.CreateAsync(mapper.Map<Account>(registerInput), registerInput.Password);

                if (!results.Succeeded)
                {
                    foreach (var error in results.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                //await context.SaveChangesAsync();

                return Accepted("User registered successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(Register)}: ");

                return StatusCode(500, $"Internal server Error");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginInput loginInput)
        {
            logger.LogInformation($"Login attempt for {loginInput.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Validate Email and Password
                if (!await authService.ValidateUserWithPassword(loginInput))
                {
                    return Unauthorized("Incorrect Username or password");
                }

                var account = await accountService.FindAccountByEmailAsync(loginInput.Email);
                //var roles = await accountService.GetRolesAsync(user);

                //
                var token = await authService.GenerateJwtToken(account);

                var results = new { jwtToken = "Bearer " + token, id = account.Id };
                //var results = new { jwtToken = "Bearer " + token, roles = roles, id = account.Id };

                return Accepted(results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(Login)}: ");

                return StatusCode(500, $"Internal server Error");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("profile/{id}")]
        public async Task<IActionResult> Profile(string id)
        {
            logger.LogInformation($"Retrieve Profile by id: {id}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var account = await accountService.FindAccountByIdAsync(id);

                if(account == null)
                {
                    return BadRequest("Could not find user");
                }

                var profile = mapper.Map<ProfileDto>(account);
                //Configure Roles || fetch role
                profile.Roles = ["student"];

                return Ok(profile);     
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(Profile)}: ");

                return StatusCode(500, $"Internal server Error");
            }
        }


        [Authorize, HttpDelete("logout")]
        private async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<StudentDto>> Update(string id, [FromBody] AccountDto accountDto)
        {

            if (id != accountDto.Id)
                return BadRequest("Content did not match subject");

            try
            {
                await accountService.UpdateAsync(accountDto);

                var account = await accountService.FindAccountByIdAsync(id);

                var profile = mapper.Map<ProfileDto>(account);

                return Accepted(profile);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EntryExist(id))
                    return NotFound(ex);
                else
                    throw;
            }

           
        }

        private bool EntryExist(string id)
        {
            return (context.Accounts?.Any(account => account.Id == id)).GetValueOrDefault();
        }
    }
}
