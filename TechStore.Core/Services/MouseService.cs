using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Mice;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Core.Services
{
	public class MouseService : IMouseService
	{
		private readonly IRepository repository;
		private readonly IGuard guard;

		public MouseService(IRepository repository, IGuard guard)
		{
			this.repository = repository;
			this.guard = guard;
		}

		public async Task<int> AddMouseAsync(MouseImportViewModel model, string? userId)
		{
			var mouse = new Mouse()
			{
				ImageUrl = model.ImageUrl,
				Warranty = model.Warranty,
				Price = model.Price != null ? model.Price.Value : default,
				Quantity = model.Quantity != null ? model.Quantity.Value : default,
				IsWireless = model.IsWireless != null ? model.IsWireless.Value : default,

				IsDeleted = false,
				AddedOn = DateTime.UtcNow.Date,
			};

			var sensitivity = await this.repository.GetByPropertyAsync<Sensitivity>(s => s.Range == model.Sensitivity);

			this.guard.AgainstNotExistingValue<Sensitivity>(sensitivity, ErrorMessageForNotExistingValue);

			mouse.Sensitivity = sensitivity;

			Client? dbClient = null;

			if (userId is not null)
			{
				dbClient = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

				this.guard.AgainstInvalidUserId<Client>(dbClient, ErrorMessageForInvalidUserId);
			}

			mouse.Seller = dbClient;

			mouse = await this.SetNavigationPropertiesAsync(
				mouse,
				model.Brand,
				model.Type,
				model.Color);

			await this.repository.AddAsync<Mouse>(mouse);

			await this.repository.SaveChangesAsync();

			return mouse.Id;
		}

		public async Task DeleteMouseAsync(int id)
		{
			var mouse = await this.repository.GetByIdAsync<Mouse>(id);

			this.guard.AgainstProductThatIsNull<Mouse>(mouse, ErrorMessageForInvalidProductId);

			this.guard.AgainstProductThatIsDeleted(mouse.IsDeleted, ErrorMessageForDeletedProduct);

			mouse.IsDeleted = true;

			await this.repository.SaveChangesAsync();
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

		public async Task<MouseDetailsExportViewModel> GetMouseByIdAsMouseDetailsExportViewModelAsync(int id)
		{
			var mouseExports = await this.GetMiceAsMouseDetailsExportViewModelsAsync<Mouse>(m => m.Id == id);

			this.guard.AgainstNullOrEmptyCollection<MouseDetailsExportViewModel>(mouseExports, ErrorMessageForInvalidProductId);

			return mouseExports.First();
		}

		private async Task<IList<MouseDetailsExportViewModel>> GetMiceAsMouseDetailsExportViewModelsAsync<T>(
			Expression<Func<Mouse, bool>> condition)
		{
			var miceAsMouseDetailsExportViewModels = await this.repository
				.AllAsReadOnly<Mouse>(m => !m.IsDeleted)
				.Where(condition)
				.Select(m => new MouseDetailsExportViewModel()
				{
					Id = m.Id,
					Brand = m.Brand.Name,
					Price = m.Price,
					IsWireless = m.IsWireless,
					Type = m.Type.Name,
					Sensitivity = m.Sensitivity.Range,
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

			return miceAsMouseDetailsExportViewModels;
		}

		private async Task<Mouse> SetNavigationPropertiesAsync(
			Mouse mouse,
			string brand,
			string type,
			string? color)
		{
			var brandNormalized = brand.ToLower();
			var dbBrand = await this.repository.GetByPropertyAsync<Brand>(b => EF.Functions.Like(b.Name.ToLower(), brandNormalized));
			dbBrand ??= new Brand { Name = brand };
			mouse.Brand = dbBrand;

			var typeNormalized = type.ToLower();
			var dbType = await this.repository.GetByPropertyAsync<Type>(t => EF.Functions.Like(t.Name.ToLower(), typeNormalized));
			dbType ??= new Type { Name = type };
			mouse.Type = dbType;

			if (String.IsNullOrWhiteSpace(color))
			{
				mouse.Color = null;
			}
			else
			{
				var colorNormalized = color.ToLower();
				var dbColor = await this.repository.GetByPropertyAsync<Color>(c => EF.Functions.Like(c.Name.ToLower(), colorNormalized));
				dbColor ??= new Color { Name = color };
				mouse.Color = dbColor;
			}

			return mouse;
		}
	}
}
