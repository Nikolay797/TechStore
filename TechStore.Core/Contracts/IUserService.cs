using TechStore.Core.Models.User;

namespace TechStore.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserExportViewModel>> GetAllUsersThatAreNotInTheSpecifiedRole(string? roleId);
    }
}
