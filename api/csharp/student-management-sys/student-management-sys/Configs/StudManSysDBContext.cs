using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using student_management_sys.Entity;
using System.Reflection.Emit;


namespace student_management_sys.Configs
{
    public class StudManSysDBContext : IdentityDbContext<Account>
    {
        public StudManSysDBContext(DbContextOptions<StudManSysDBContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
            .HasOne(s => s.Account)
            .WithOne(a => a.Student)
            .HasForeignKey<Account>(a => a.StudentId);

            //builder.Entity<Parent>()
            //.HasOne(p => p.Account)
            //.WithOne(a => a.Parent)
            //.HasForeignKey<Account>(a => a.ParentId);

            // Create an admin user
            var hasher = new PasswordHasher<Account>();

            var adminAccount = new Account
            {
                Id = "1",
                FirstName = "Admin",
                LastName = "Test",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@company.com",
                NormalizedEmail = "ADMIN@COMPANY.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123")
            };

            builder.Entity<Account>().HasData(adminAccount);

            // Assign the admin user to the Admin role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" }
            );

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfig());
        }

        
    }
}
