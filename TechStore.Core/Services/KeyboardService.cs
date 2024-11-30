using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Models.Keyboard;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;

namespace TechStore.Core.Services
{
    public class KeyboardService : IKeyboardService
    {
        private readonly IRepository repository;

        public KeyboardService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<KeyboardsQueryModel> GetAllKeyboardsAsync(string? format = null, string? type = null,
            Wireless wireless = Wireless.No, string? keyword = null, Sorting sorting = Sorting.Newest,
            int currentPage = 1)
        {
            var result = new KeyboardsQueryModel();

            var query = this.repository.AllAsReadOnly<Keyboard>(k => !k.IsDeleted);

            if (!String.IsNullOrEmpty(format))
            {
                query = query.Where(k => k.Format != null && k.Format.Name == format);
            }

            if (!String.IsNullOrEmpty(type))
            {
                query = query.Where(k => k.Type.Name == type);
            }

            query = wireless switch
            {
                Wireless.No => query.Where(k => !k.IsWireless),

                Wireless.Yes => query.Where(k => k.IsWireless),

                _ => query
            };

            if (!String.IsNullOrEmpty(keyword))
            {
                var searchTerm = $"%{keyword.ToLower()}%";

                query = query.Where(k => EF.Functions.Like(k.Brand.Name.ToLower(), searchTerm)
                                         || EF.Functions.Like(k.Type.Name.ToLower(), searchTerm)
                                         || (k.Format != null && EF.Functions.Like(k.Format.Name.ToLower(), searchTerm)));
            }

            query = sorting switch
            {
                Sorting.Brand => query.OrderBy(k => k.Brand.Name),

                Sorting.PriceMinToMax => query.OrderBy(k => k.Price),

                Sorting.PriceMaxToMin => query.OrderByDescending(k => k.Price),

                _ => query.OrderByDescending(k => k.Id)
            };

            result.Keyboards = await query
                .Skip((currentPage - 1) * ProductsPerPage)
                .Take(ProductsPerPage)
                .Select(k => new KeyboardExportViewModel()
                {
                    Id = k.Id,
                    Brand = k.Brand.Name,
                    Type = k.Type.Name,
                    Format = k.Format != null ? k.Format.Name : UnknownCharacteristic,
                    Wireless = k.IsWireless,
                    Price = k.Price,
                    Warranty = k.Warranty,
                })
                .ToListAsync();

            result.TotalKeyboardsCount = await query.CountAsync();

            return result;
        }

        public async Task<IEnumerable<string>> GetAllKeyboardsFormatsAsync()
        {
            return await this.repository.AllAsReadOnly<Keyboard>(k => !k.IsDeleted)
                .Where(k => k.Format != null)
                .Select(k => k.Format.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllKeyboardsTypesAsync()
        {
            return await this.repository.AllAsReadOnly<Keyboard>(k => !k.IsDeleted)
                .Select(k => k.Type.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
        }
    }
}
