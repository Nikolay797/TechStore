using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Smartwatch;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using System.Linq.Expressions;
using System.Globalization;
using TechStore.Infrastructure.Data.Models.AttributesClasses;

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

		public async Task<int> AddSmartwatchAsync(SmartwatchImportViewModel model, string? userId)
		{
			var smartwatch = new SmartWatch()
			{
				ImageUrl = model.ImageUrl,
				Warranty = model.Warranty,
				Price = model.Price != null ? model.Price.Value : default,
				Quantity = model.Quantity != null ? model.Quantity.Value : default,

				IsDeleted = false,
				AddedOn = DateTime.UtcNow.Date,
			};

			Client? dbClient = null;

			if (userId is not null)
			{
				dbClient = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

				this.guard.AgainstInvalidUserId<Client>(dbClient, ErrorMessageForInvalidUserId);
			}

			smartwatch.Seller = dbClient;

			smartwatch = await this.SetNavigationPropertiesAsync(smartwatch, model.Brand, model.Color);

			await this.repository.AddAsync<SmartWatch>(smartwatch);

			await this.repository.SaveChangesAsync();

			return smartwatch.Id;
		}

		public async Task DeleteSmartwatchAsync(int id)
		{
			var smartwatch = await this.repository.GetByIdAsync<SmartWatch>(id);

			this.guard.AgainstProductThatIsNull<SmartWatch>(smartwatch, ErrorMessageForInvalidProductId);

			this.guard.AgainstProductThatIsDeleted(smartwatch.IsDeleted, ErrorMessageForDeletedProduct);

			smartwatch.IsDeleted = true;

			await this.repository.SaveChangesAsync();
		}

		public async Task<int> EditSmartwatchAsync(SmartwatchEditViewModel model)
		{
			var smartwatch = await this.repository
				.All<SmartWatch>(m => !m.IsDeleted)
				.Where(m => m.Id == model.Id)
				.Include(m => m.Brand)
				.Include(m => m.Color)
				.FirstOrDefaultAsync();

			this.guard.AgainstProductThatIsNull<SmartWatch>(smartwatch, ErrorMessageForInvalidProductId);

			smartwatch.ImageUrl = model.ImageUrl;
			smartwatch.Warranty = model.Warranty;
			smartwatch.Price = model.Price != null ? model.Price.Value : default;
			smartwatch.Quantity = model.Quantity != null ? model.Quantity.Value : default;
			smartwatch.AddedOn = DateTime.UtcNow.Date;

			smartwatch = await this.SetNavigationPropertiesAsync(smartwatch, model.Brand, model.Color);

			await this.repository.SaveChangesAsync();

			return smartwatch.Id;
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

		public async Task<SmartwatchEditViewModel> GetSmartwatchByIdAsSmartwatchEditViewModelAsync(int id)
		{
			var smartwatchExport = await this.repository
				.AllAsReadOnly<SmartWatch>(m => !m.IsDeleted)
				.Where(m => m.Id == id)
				.Select(m => new SmartwatchEditViewModel()
				{
					Id = m.Id,
					Brand = m.Brand.Name,
					Quantity = m.Quantity,
					Price = m.Price,
					Warranty = m.Warranty,
					Color = m.Color == null ? null : m.Color.Name,
					ImageUrl = m.ImageUrl,
					Seller = m.Seller,
				})
				.FirstOrDefaultAsync();

			this.guard.AgainstProductThatIsNull<SmartwatchEditViewModel>(smartwatchExport, ErrorMessageForInvalidProductId);

			return smartwatchExport;
		}

		private async Task<SmartWatch> SetNavigationPropertiesAsync(SmartWatch smartwatch, string brand, string? color)
		{
			var brandNormalized = brand.ToLower();
			var dbBrand = await this.repository.GetByPropertyAsync<Brand>(b => EF.Functions.Like(b.Name.ToLower(), brandNormalized));
			dbBrand ??= new Brand { Name = brand };
			smartwatch.Brand = dbBrand;

			if (String.IsNullOrWhiteSpace(color))
			{
				smartwatch.Color = null;
			}
			else
			{
				var colorNormalized = color.ToLower();
				var dbColor = await this.repository.GetByPropertyAsync<Color>(c => EF.Functions.Like(c.Name.ToLower(), colorNormalized));
				dbColor ??= new Color { Name = color };
				smartwatch.Color = dbColor;
			}

			return smartwatch;
		}
	}
}
