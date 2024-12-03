using TechStore.Core.Models.User;
using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Core.Contracts
{
	public interface IAdminUserService
	{
		Task<IEnumerable<UserExportViewModel>> GetAllUsersThatAreNotInTheSpecifiedRole(string? roleId);
		Task<User> PromoteToAdminAsync(string id);
	}
}
