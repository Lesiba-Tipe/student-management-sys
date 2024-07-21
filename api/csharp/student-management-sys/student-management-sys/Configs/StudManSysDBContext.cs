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

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfig());
        }
    }
}
