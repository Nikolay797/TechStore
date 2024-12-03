using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.User;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models.Account;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Core.Services
{
	public class AdminUserService : IAdminUserService
	{
		private readonly IRepository repository;
		private readonly UserManager<User> userManager;
		private readonly IGuard guard;

		public AdminUserService(IRepository repository, UserManager<User> userManager, IGuard guard)
		{
			this.repository = repository;
			this.userManager = userManager;
			this.guard = guard;
		}

		public async Task<IEnumerable<UserExportViewModel>> GetAllUsersThatAreNotInTheSpecifiedRole(string? roleId)
		{
			var users = await this.repository.AllAsReadOnly<User>()
				.Select(u => new UserExportViewModel()
				{
					Id = u.Id,
					Username = u.UserName ?? UnknownCharacteristic,
					Email = u.Email ?? UnknownCharacteristic,
					FirstName = u.FirstName ?? UnknownCharacteristic,
					LastName = u.LastName ?? UnknownCharacteristic,
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

		public async Task<User> PromoteToAdminAsync(string id)
		{
			var user = await this.userManager.FindByIdAsync(id);

			this.guard.AgainstInvalidUserId<User>(user, ErrorMessageForInvalidUserId);

			await this.userManager.AddToRoleAsync(user, Administrator);

			return user;
		}
	}
}
