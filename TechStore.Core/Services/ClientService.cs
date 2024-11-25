using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;

namespace TechStore.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository repository;
        private readonly IGuard guard;

        public ClientService(IRepository repository, IGuard guard)
        {
            this.repository = repository;
            this.guard = guard;
        }

        public async Task<int> GetNumberOfActiveSales(string userId)
        {
            var client = await this.repository
                .AllAsReadOnly<Client>(c => c.UserId == userId)
                .Include(c => c.Laptops)
                .Include(c => c.Televisions)
                .Include(c => c.Keyboards)
                .Include(c => c.Mice)
                .Include(c => c.Headphones)
                .Include(c => c.SmartWatches)
                .FirstOrDefaultAsync();

            this.guard.AgainstNull<Client>(client, ErrorMessageForInvalidUserId);

            var numberOfClientSales = client.Laptops.Where(l => !l.IsDeleted).Count()
                                      + client.Televisions.Where(t => !t.IsDeleted).Count()
                                      + client.Keyboards.Where(k => !k.IsDeleted).Count()
                                      + client.Mice.Where(m => !m.IsDeleted).Count()
                                      + client.Headphones.Where(h => !h.IsDeleted).Count()
                                      + client.SmartWatches.Where(s => !s.IsDeleted).Count();

            return numberOfClientSales;
        }
    }
}