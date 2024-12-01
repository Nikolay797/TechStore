﻿using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Models.Mice;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;

namespace TechStore.Core.Services
{
	public class MouseService : IMouseService
	{
		private readonly IRepository repository;

		public MouseService(IRepository repository)
		{
			this.repository = repository;
		}

		public async Task<MiceQueryModel> GetAllMiceAsync(
			string? type = null,
			string? sensitivity = null,
			Wireless wireless = Wireless.Regardless,
			string? keyword = null,
			Sorting sorting = Sorting.Newest,
			int currentPage = 1)
		{
			var result = new MiceQueryModel();

			var query = this.repository.AllAsReadOnly<Mouse>(m => !m.IsDeleted);

			if (!String.IsNullOrEmpty(type))
			{
				query = query.Where(m => m.Type.Name == type);
			}

			if (!String.IsNullOrEmpty(sensitivity))
			{
				query = query.Where(m => m.Sensitivity.Range == sensitivity);
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

				query = query.Where(m => EF.Functions.Like(m.Brand.Name.ToLower(), searchTerm)
				                         || EF.Functions.Like(m.Type.Name.ToLower(), searchTerm)
				                         || EF.Functions.Like(m.Sensitivity.Range.ToLower(), searchTerm));
			}

			query = sorting switch
			{
				Sorting.Brand => query.OrderBy(m => m.Brand.Name),

				Sorting.PriceMinToMax => query.OrderBy(m => m.Price),

				Sorting.PriceMaxToMin => query.OrderByDescending(m => m.Price),

				_ => query.OrderByDescending(m => m.Id)
			};

			result.Mice = await query
				.Skip((currentPage - 1) * ProductsPerPage)
				.Take(ProductsPerPage)
				.Select(m => new MouseExportViewModel()
				{
					Id = m.Id,
					Brand = m.Brand.Name,
					Type = m.Type.Name,
					Sensitivity = m.Sensitivity.Range,
					IsWireless = m.IsWireless,
					Price = m.Price,
					Warranty = m.Warranty,
				})
				.ToListAsync();

			result.TotalMiceCount = await query.CountAsync();

			return result;
		}

		public async Task<IEnumerable<string>> GetAllMiceSensitivitiesAsync()
		{
			return await this.repository.AllAsReadOnly<Mouse>(m => !m.IsDeleted)
				.Select(m => m.Sensitivity.Range)
				.Distinct()
				.OrderBy(r => r)
				.ToListAsync();
		}

		public async Task<IEnumerable<string>> GetAllMiceTypesAsync()
		{
			return await this.repository.AllAsReadOnly<Mouse>(m => !m.IsDeleted)
				.Select(m => m.Type.Name)
				.Distinct()
				.OrderBy(n => n)
				.ToListAsync();
		}
	}
}
