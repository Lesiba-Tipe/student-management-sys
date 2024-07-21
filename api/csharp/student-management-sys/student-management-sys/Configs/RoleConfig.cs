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
                    new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "2", Name = "Parent", NormalizedName = "PARENT" },
                    new IdentityRole { Id = "3", Name = "Student", NormalizedName = "STUDENT" },
                    new IdentityRole { Id = "4", Name = "Primary", NormalizedName = "PRIMARY" }
                );
            
        }
    }
}
