using TechStore.Core.Models.User;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserExportViewModel>> GetAllUsersThatAreNotInTheSpecifiedRole(string? roleId);
        Task<bool> ShouldBePromotedToBestUser(Client client);
        Task<User> PromoteToAdminAsync(string id);
    }
}
