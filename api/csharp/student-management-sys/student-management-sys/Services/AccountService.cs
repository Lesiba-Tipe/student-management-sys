using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using student_management_sys.Configs;
using student_management_sys.Dto;
using student_management_sys.Entity;
using student_management_sys.Inputs;

namespace student_management_sys.Services
{
    public class AccountService
    {
        private readonly UserManager<Account> accountManager;
        private readonly StudManSysDBContext context;
        public readonly IMapper mapper;
        public AccountService(UserManager<Account> accountManager, StudManSysDBContext context) 
        { 
            this.accountManager = accountManager;
            this.context = context;
        }

        public AccountService(UserManager<Account> accountManager, StudManSysDBContext context, IMapper mapper)
        {
            this.accountManager = accountManager;
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<IdentityResult> CreateAsync(Account account, string password) //CREATE NEW ACCOUNT
        {
            //Make username same as email
            account.UserName = account.Email;
            account.Id = Guid.NewGuid().ToString();
            return await accountManager.CreateAsync(account, password);
        }

        public async Task<int> UpdateAsync(AccountDto accountDto)
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

            context.Entry(entity).State = EntityState.Modified;

            var results = await context.SaveChangesAsync();

            return results;
        }

        public async Task<Account> FindAccountByEmailAsync(string email)
        {
            var account = await accountManager.FindByEmailAsync(email);
            return account;
        }

        public async Task<Account> FindAccountByIdAsync(string id)
        {
            var account = await accountManager.FindByIdAsync(id);
            return account;
        }

        //public async Task<Account> AccountExistAsync(string idNumber)
        //{
        //    var account = await context.Accounts.Where(acc => acc.IdNumber == idNumber).FirstOrDefaultAsync();
        //    return account;
        //}

        public async Task<IdentityResult> AddRoles(RoleDto roleDto)
        {
            if (roleDto.Roles == null)
                return null; //roleDto.Roles.Add("Primary");   //Set Default Role if Null

            var account = await FindAccountByIdAsync(roleDto.Id);

            var results = await accountManager.AddToRolesAsync(account, roleDto.Roles);

            return results;
        }

        public async Task<IList<string>> GetRolesAsync(Account account)
        {
            return await accountManager.GetRolesAsync(account);
        }

    }
}
