﻿using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Models.Television;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using Television = TechStore.Infrastructure.Data.Models.Television;

namespace TechStore.Core.Services
{
    public class TelevisionService : ITelevisionService
    {
        private readonly IRepository repository;

        public TelevisionService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<string>> GetAllBrandsNames()
        {
            return await this.repository.AllAsReadOnly<Television>(m => !m.IsDeleted)
                .Select(m => m.Brand.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
        }

        public async Task<IEnumerable<double>> GetAllDisplaysSizesValues()
        {
            return await this.repository.AllAsReadOnly<Television>(m => !m.IsDeleted)
                .Select(m => m.DisplaySize.Value)
                .Distinct()
                .OrderBy(v => v)
                .ToListAsync();
        }

        public async Task<TelevisionsQueryModel> GetAllTelevisionsAsync(
            string? brand = null,
            double? displaySize = null,
            string? resolution = null,
            string? keyword = null,
            Sorting sorting = Sorting.PriceMinToMax,
            int currentPage = 1)

        {
            var result = new TelevisionsQueryModel();
            var query = this.repository.AllAsReadOnly<Television>(m => !m.IsDeleted);

            if (!String.IsNullOrEmpty(brand))
            {
                query = query.Where(m => m.Brand.Name == brand);
            }

            if (displaySize is not null)
            {
                query = query.Where(m => m.DisplaySize.Value == displaySize);
            }

            if (!String.IsNullOrEmpty(resolution))
            {
                query = query.Where(m => m.Resolution.Value == resolution);
            }

            if (!String.IsNullOrEmpty(keyword))
            {
                var searchTerm = $"%{keyword.ToLower()}%";

                query = query.Where(m => EF.Functions.Like(m.Brand.Name.ToLower(), searchTerm)
                                         || EF.Functions.Like(m.Type.Name.ToLower(), searchTerm)
                                         || (m.DisplayTechnology != null && EF.Functions.Like(m.DisplayTechnology.Name.ToLower(), searchTerm)));
            }

            query = sorting switch
            {
                Sorting.Brand => query.OrderBy(m => m.Brand.Name),
                Sorting.PriceMinToMax => query.OrderBy(m => m.Price),
                Sorting.PriceMaxToMin => query.OrderByDescending(m => m.Price),
                _ => query.OrderByDescending(m => m.Id)
            };

            result.Televisions = await query
                .Skip((currentPage - 1) * ProductsPerPage)
                .Take(ProductsPerPage)
                .Select(m => new TelevisionExportViewModel()
                {
                    Id = m.Id,
                    Brand = m.Brand.Name,
                    DisplaySize = m.DisplaySize.Value,
                    DisplayTechnology = m.DisplayTechnology != null
                        ? m.DisplayTechnology.Name
                        : UnknownProductCharacteristic,
                    Resolution = m.Resolution.Value,
                    Price = m.Price,
                    Warranty = m.Warranty,
                })
                .ToListAsync();
            result.TotalTelevisionsCount = await query.CountAsync();
            return result;

        }

        public async Task<IEnumerable<string>> GetAllResolutionsValues()
        {
            return await this.repository.AllAsReadOnly<Television>(m => !m.IsDeleted)
                .Select(m => m.Resolution.Value)
                .Distinct()
                .OrderBy(v => v)
                .ToListAsync();
        }
    }
}
