using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Contracts
{
    public interface IUserService
    {
	    Task<bool> ShouldBePromotedToBestUser(Client client);
    }
}
