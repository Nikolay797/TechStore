using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Models.Headphone;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;

namespace TechStore.Core.Services
{
	public class HeadphoneService : IHeadphoneService
	{
		private readonly IRepository repository;

		public HeadphoneService(IRepository repository)
		{
			this.repository = repository;
		}

		public async Task<HeadphonesQueryModel> GetAllHeadphonesAsync(
			string? type = null,
			Wireless wireless = Wireless.Regardless,
			string? keyword = null,
			Sorting sorting = Sorting.Newest,
			int currentPage = 1)
		{
			var result = new HeadphonesQueryModel();

			var query = this.repository.AllAsReadOnly<Headphone>(h => !h.IsDeleted);

			if (!String.IsNullOrEmpty(type))
			{
				query = query.Where(h => h.Type.Name == type);
			}

			query = wireless switch
			{
				Wireless.No => query.Where(h => !h.IsWireless),

				Wireless.Yes => query.Where(h => h.IsWireless),

				_ => query
			};

			if (!String.IsNullOrEmpty(keyword))
			{
				var searchTerm = $"%{keyword.ToLower()}%";

				query = query.Where(h => EF.Functions.Like(h.Brand.Name.ToLower(), searchTerm)
				                         || EF.Functions.Like(h.Type.Name.ToLower(), searchTerm));
			}

			query = sorting switch
			{
				Sorting.Brand => query.OrderBy(h => h.Brand.Name),

				Sorting.PriceMinToMax => query.OrderBy(h => h.Price),

				Sorting.PriceMaxToMin => query.OrderByDescending(h => h.Price),

				_ => query.OrderByDescending(h => h.Id)
			};

			result.Headphones = await query
				.Skip((currentPage - 1) * ProductsPerPage)
				.Take(ProductsPerPage)
				.Select(h => new HeadphoneExportViewModel()
				{
					Id = h.Id,
					Brand = h.Brand.Name,
					Type = h.Type.Name,
					IsWireless = h.IsWireless,
					HasMicrophone = h.HasMicrophone,
					Price = h.Price,
					Warranty = h.Warranty,
				})
				.ToListAsync();

			result.TotalHeadphonesCount = await query.CountAsync();

			return result;
		}

		public async Task<IEnumerable<string>> GetAllHeadphonesTypesAsync()
		{
			return await this.repository.AllAsReadOnly<Headphone>(h => !h.IsDeleted)
				.Select(h => h.Type.Name)
				.Distinct()
				.OrderBy(n => n)
				.ToListAsync();
		}
	}
}
