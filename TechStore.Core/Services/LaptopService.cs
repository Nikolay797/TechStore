using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Models.Laptop;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly IRepository repository;

        public LaptopService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<LaptopExportViewModel>> GetAllLaptopsAsync()
        {
            return await this.repository.AllAsReadOnly<Laptop>(l => !l.IsDeleted && l.Quantity >= 1)
                .Select(l => new LaptopExportViewModel()
                {
                    Id = l.Id,
                    Brand = l.Brand.Name,
                    CPU = l.CPU.Name,
                    RAM = l.RAM.Value,
                    SSDCapacity = l.SSDCapacity.Value,
                    VideoCard = l.VideoCard.Name,
                    Price = l.Price,
                    DisplaySize = l.DisplaySize.Value,
                    Warranty = l.Warranty,
                })
                .ToListAsync();
        }
    }
}
