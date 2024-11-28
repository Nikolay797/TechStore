using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static TechStore.Infrastructure.Constants.DataConstant.UserConstants;

using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(this.CreateUsers());
        }

        private List<User> CreateUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "69d44205-edfe-47b9-8d27-6366c018f434",
                UserName = AdminUserName,
                NormalizedUserName = AdminUserName.ToUpper(),
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                FirstName = "Admin-FN",
                LastName = "Admin-LN",
            };
            user.PasswordHash = hasher.HashPassword(user, "admin123");
            users.Add(user);

            user = new User()
            {
                Id = "0b129438-03c0-4f93-8d80-16fa6d4afa54",
                UserName = BestUserUserName,
                NormalizedUserName = BestUserUserName.ToUpper(),
                Email = "bestUser@mail.com",
                NormalizedEmail = "BESTUSER@MAIL.COM",
                FirstName = "BestUser-FN",
                LastName = "BestUser-LN",
            };
            user.PasswordHash = hasher.HashPassword(user, "bestUser123");
            users.Add(user);

            user = new User()
            {
                Id = "80c1bdd8-73f9-4713-a939-090e9e07281b",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@mail.com",
                NormalizedEmail = "USER@MAIL.COM",
                FirstName = "User-FN",
                LastName = "User-LN",
            };
            user.PasswordHash = hasher.HashPassword(user, "user123");
            users.Add(user);

            return users;
        }
    }
}
