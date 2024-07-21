using Microsoft.AspNetCore.Identity;
using student_management_sys.Configs;
using student_management_sys.Dto;
using student_management_sys.Entity;

namespace student_management_sys.Services
{
    public class AccountService
    {
        private readonly UserManager<Account> accountManager;
        private readonly StudManSysDBContext context;

        public AccountService(UserManager<Account> accountManager, StudManSysDBContext context) 
        { 
            this.accountManager = accountManager;
            this.context = context;
        }

        
        public async Task<IdentityResult> CreateAsync(Account account, string password) //CREATE NEW ACCOUNT
        {
            account.Id = Guid.NewGuid().ToString();
            return await accountManager.CreateAsync(account, password);
        }

        public async Task<int> Update(AccountDto accountDto)
        {
            var entity = context.Users.First(x => x.Id == accountDto.Id);

            if (entity == null)
                return 0;

            if (accountDto.FirstName != null)
                entity.FirstName = accountDto.FirstName;

            if (accountDto.LastName != null)
                entity.LastName = accountDto.LastName;


            if (accountDto.PhoneNumber != null)
                entity.PhoneNumber = accountDto.PhoneNumber;

            //Edit Physical Address object here...

            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            

            var results = await context.SaveChangesAsync();

            return results;
        }

        
    }
}
