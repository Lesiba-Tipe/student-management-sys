using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using student_management_sys.Entity;
using student_management_sys.Inputs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace student_management_sys.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Account> accountManager;
        private IConfigurationSection jwtOptions;

        public AuthService(UserManager<Account> accountManager, IConfiguration configuration) 
        {
            this.accountManager = accountManager;
        }
        public async Task<string> GenerateJwtToken(Account account)
        {
            var key = Encoding.ASCII.GetBytes(jwtOptions.GetSection("Key").Value);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var claims = await GetClaims(account);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var results = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return results;
        }

        public async Task<bool> ValidateUserWithPassword(LoginInput loginInput)
        {
            //Find username
            var account = await accountManager.FindByEmailAsync(loginInput.Email);
            return (account != null && await accountManager.CheckPasswordAsync(account, loginInput.Password));
        }

        private async Task<List<Claim>> GetClaims(Account account)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, account.UserName)
            };

            var roles = await accountManager.GetRolesAsync(account);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: jwtOptions.GetSection("Issuer").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
                );

            return token;
        }
    }
}
