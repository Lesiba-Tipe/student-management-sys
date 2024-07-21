using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace student_management_sys.Configs
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "Parent", NormalizedName = "PARENT" },
                    new IdentityRole { Name = "Student", NormalizedName = "STUDENT" },
                    new IdentityRole { Name = "Primary", NormalizedName = "PRIMARY" }
                );
            
        }
    }
}
