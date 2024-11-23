using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Infrastructure.Data.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(this.CreateRoles());
        }

        private List<IdentityRole> CreateRoles()
        {
            var roles = new List<IdentityRole>();

            var role = new IdentityRole()
            {
                Id = "c6e8e5fc-103e-4c47-804a-0fc166c6e506",
                Name = Administrator,
                NormalizedName = Administrator.ToUpper(),
            };
            roles.Add(role);

            role = new IdentityRole()
            {
                Id = "35d3a37a-f7f6-49a3-8022-8bd9044e90ac",
                Name = BestUser,
                NormalizedName = BestUser.ToUpper(),
            };
            roles.Add(role);

            return roles;
        }
    }
}
