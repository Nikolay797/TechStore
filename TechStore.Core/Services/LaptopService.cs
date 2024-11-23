using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

        public async Task DeleteLaptopAsync(int id)
        {
            var laptop = await this.repository
                    .All<Laptop>(l => !l.IsDeleted)
                    .FirstOrDefaultAsync(l => l.Id == id);

                if (laptop is null)
                {
                    throw new ArgumentException("Invalid laptop id!");
                }

                laptop.IsDeleted = true;

                await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LaptopExportViewModel>> GetAllLaptopsAsync()
        {
            return await this.repository
                .AllAsReadOnly<Laptop>(l => !l.IsDeleted && l.Quantity >= 1)
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

        public async Task<LaptopDetailsExportViewModel> GetLaptopByIdAsDtoAsync(int id)
        {
            var laptopExport = await this.repository
                .AllAsReadOnly<Laptop>(l => !l.IsDeleted)
                .Where(l => l.Id == id)
                .Select(l => new LaptopDetailsExportViewModel()
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
                    Type = l.Type.Name,
                    DisplayCoverage = l.DisplayCoverage != null ? l.DisplayCoverage.Name : "unknown",
                    DisplayTechnology = l.DisplayTechnology != null ? l.DisplayTechnology.Name : "unknown",
                    Resolution = l.Resolution != null ? l.Resolution.Value : "unknown",
                    Color = l.Color != null ? l.Color.Name : "unknown",
                    ImageUrl = l.ImageUrl,
                    AddedOn = l.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture),
                    Quantity = l.Quantity,
                })
                .FirstOrDefaultAsync();

            if (laptopExport is null)
            {
                throw new ArgumentException("Invalid Laptop Id!");
            }

            return laptopExport;
        }
    }
}