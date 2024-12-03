using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Smartwatch;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using System.Linq.Expressions;
using System.Globalization;

namespace TechStore.Core.Services
{
	public class SmartwatchService : ISmartwatchService
	{
		private readonly IRepository repository;
		private readonly IGuard guard;

		public SmartwatchService(IRepository repository, IGuard guard)
		{
			this.repository = repository;
			this.guard = guard;
		}

		public async Task<SmartwatchesQueryModel> GetAllSmartwatchesAsync(
			string? keyword = null,
			Sorting sorting = Sorting.Newest,
			int currentPage = 1)
		{
			var result = new SmartwatchesQueryModel();

			var query = this.repository.AllAsReadOnly<SmartWatch>(m => !m.IsDeleted);

			if (!String.IsNullOrEmpty(keyword))
			{
				var searchTerm = $"%{keyword.ToLower()}%";

				query = query.Where(m => EF.Functions.Like(m.Brand.Name.ToLower(), searchTerm));
			}

			query = sorting switch
			{
				Sorting.Brand => query.OrderBy(m => m.Brand.Name),

				Sorting.PriceMinToMax => query.OrderBy(m => m.Price),

				Sorting.PriceMaxToMin => query.OrderByDescending(m => m.Price),

				_ => query.OrderByDescending(m => m.Id)
			};

			result.Smartwatches = await query
				.Skip((currentPage - 1) * ProductsPerPage)
				.Take(ProductsPerPage)
				.Select(m => new SmartwatchExportViewModel()
				{
					Id = m.Id,
					Brand = m.Brand.Name,
					Price = m.Price,
					Warranty = m.Warranty,
				})
				.ToListAsync();

			result.TotalSmartwatchesCount = await query.CountAsync();

			return result;
		}

		public async Task<SmartwatchDetailsExportViewModel> GetSmartwatchByIdAsSmartwatchDetailsExportViewModelAsync(int id)
		{
			// Използвай метода GetSmartwatchesAsSmartwatchesDetailsExportViewModelsAsync
			var smartwatchExports = await this.GetSmartwatchesAsSmartwatchesDetailsExportViewModelsAsync<SmartWatch>(m => m.Id == id);

			// Увери се, че резултатът не е празен
			this.guard.AgainstNullOrEmptyCollection<SmartwatchDetailsExportViewModel>(smartwatchExports, ErrorMessageForInvalidProductId);

			// Върни първия резултат
			return smartwatchExports.First();
		}

		public async Task<IList<SmartwatchDetailsExportViewModel>> GetSmartwatchesAsSmartwatchesDetailsExportViewModelsAsync<T>(
			Expression<Func<SmartWatch, bool>> condition)
		{
			var smartwatchesAsSmartwatchDetailsExportViewModels = await this.repository
				.AllAsReadOnly<SmartWatch>(m => !m.IsDeleted)
				.Where(condition)
				.Select(m => new SmartwatchDetailsExportViewModel
				{
					Id = m.Id,
					Brand = m.Brand.Name,
					Price = m.Price,
					Color = m.Color != null ? m.Color.Name : UnknownCharacteristic,
					ImageUrl = m.ImageUrl,
					Warranty = m.Warranty,
					Quantity = m.Quantity,
					AddedOn = m.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture),
					Seller = m.Seller,
					SellerFirstName = m.Seller != null ? m.Seller.User.FirstName : null,
					SellerLastName = m.Seller != null ? m.Seller.User.LastName : null,
				})
				.ToListAsync();

			return smartwatchesAsSmartwatchDetailsExportViewModels;
		}

	}
}
