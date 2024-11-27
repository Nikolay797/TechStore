using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Models.User;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<UserExportViewModel>> GetAllUsersThatAreNotInTheSpecifiedRole(string? roleId)
        {
            var users = await this.repository.AllAsReadOnly<User>()
                .Select(u => new UserExportViewModel()
                {
                    Id = u.Id,
                    Username = u.UserName != null ? u.UserName : "unknown",
                    Email = u.Email != null ? u.Email : "unknown",
                    FirstName = u.FirstName != null ? u.FirstName : "unknown",
                    LastName = u.LastName != null ? u.LastName : "unknown",
                    Roles = this.repository.AllAsReadOnly<IdentityUserRole<string>>()
                        .Where(x => x.UserId == u.Id)
                        .Select(x => x.RoleId)
                        .ToList(),
                })
                .OrderBy(x => x.Username)
                .ToListAsync();

            if (roleId is not null)
            {
                users = users
                    .Where(u => !u.Roles.Any(r => r == roleId))
                    .ToList();
            }

            return users;
        }
    }
}
