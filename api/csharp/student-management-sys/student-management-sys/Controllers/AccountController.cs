using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(ILogger<AccountController> logger, IMapper mapper, IAuthService authService, AccountService accountService) : base(logger, mapper)
        {
            this.authService = authService;
            this.accountService = accountService;
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
                //Login
                if (!await authService.ValidateUserWithPassword(loginInput))
                {
                    return Unauthorized("Incorrect Username or password");
                }

                //var account = await accountService.FindUserByEmailAsync(loginInput.Email);
                //var roles = await accountService.GetRolesAsync(user);

                //
                //var results = new { jwtToken = "Bearer " + await authService.GenerateJwtToken(account), roles = roles, id = account.Id };

                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong {nameof(Login)}: ");

                return StatusCode(500, $"Internal server Error");
            }
        }
    }
}
