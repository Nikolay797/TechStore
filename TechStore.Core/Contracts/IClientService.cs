using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Contracts
{
    public interface IClientService
    {
        Task<int> GetNumberOfActiveSales(string userId);
        Task<Client> BuyProduct(string userId);
	}
}
