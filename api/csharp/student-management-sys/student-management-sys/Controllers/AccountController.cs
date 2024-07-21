using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using student_management_sys.Configs;
using student_management_sys.Entity;
using student_management_sys.Inputs;
using student_management_sys.Services;


namespace student_management_sys.Controllers
{
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

                await context.SaveChangesAsync();

                return Accepted(registerInput);
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
        [Route("profile")]
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

                return Ok(account);     
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(Profile)}: ");

                return StatusCode(500, $"Internal server Error");
            }
        }
    }
}
