using student_management_sys.Entity;
using student_management_sys.Inputs;

namespace student_management_sys.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateUserWithPassword(LoginInput loginInput);
        Task<string> GenerateJwtToken(Account account);
    }
}
