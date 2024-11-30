using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Television;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using Television = TechStore.Infrastructure.Data.Models.Television;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Core.Services
{
    public class TelevisionService : ITelevisionService
    {
        private readonly IRepository repository;
        private readonly IGuard guard;

        public TelevisionService(IRepository repository, IGuard guard)
        {
            this.repository = repository;
            this.guard = guard;
        }
        public async Task<int> EditTelevisionAsync(TelevisionEditViewModel model)
        {
            var television = await this.repository
                .All<Television>(m => !m.IsDeleted)
                .Where(m => m.Id == model.Id)
                .Include(m => m.Brand)
                .Include(m => m.DisplaySize)
                .Include(m => m.Type)
                .Include(m => m.DisplayTechnology)
                .Include(m => m.Resolution)
                .Include(m => m.Color)
                .FirstOrDefaultAsync();

            this.guard.AgainstProductThatIsNull<Television>(television, ErrorMessageForInvalidProductId);

            television.ImageUrl = model.ImageUrl;
            television.Warranty = model.Warranty;
            television.Price = model.Price;
            television.Quantity = model.Quantity;
            television.AddedOn = DateTime.UtcNow.Date;
            television = await this.SetNavigationPropertiesAsync(television, model.Brand, model.DisplaySize, model.Resolution, model.Type, model.DisplayTechnology, model.Color);
            await this.repository.SaveChangesAsync();
            return television.Id;
        }

        public async Task<IEnumerable<string>> GetAllBrandsNamesAsync()
        {
            return await this.repository.AllAsReadOnly<Television>(m => !m.IsDeleted)
                .Select(m => m.Brand.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
        }

        public async Task<IEnumerable<double>> GetAllDisplaysSizesValuesAsync()
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
                        : UnknownCharacteristic,
                    Resolution = m.Resolution.Value,
                    Price = m.Price,
                    Warranty = m.Warranty,
                })
                .ToListAsync();
            result.TotalTelevisionsCount = await query.CountAsync();
            return result;

        }

        public async Task<IEnumerable<string>> GetAllResolutionsValuesAsync()
        {
            return await this.repository.AllAsReadOnly<Television>(m => !m.IsDeleted)
                .Select(m => m.Resolution.Value)
                .Distinct()
                .OrderBy(v => v)
                .ToListAsync();
        }

        public async Task<TelevisionDetailsExportViewModel> GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(int id)
        {
			var televisionExports = await this.GetTelevisionsAsTelevisionsDetailsExportViewModelsAsync<Television>(t => t.Id == id);

			this.guard.AgainstNullOrEmptyCollection<TelevisionDetailsExportViewModel>(televisionExports, ErrorMessageForInvalidProductId);

            return televisionExports.First();
        }

        public async Task<TelevisionEditViewModel> GetTelevisionByIdAsTelevisionEditViewModelAsync(int id)
        {
            var televisionExport = await this.repository
                .All<Television>(m => !m.IsDeleted)
                .Where(m => m.Id == id)
                .Select(m => new TelevisionEditViewModel()
                {
                    Id = m.Id,
                    Brand = m.Brand.Name,
                    DisplaySize = m.DisplaySize.Value,
                    Resolution = m.Resolution.Value,
                    Type = m.Type.Name,
                    Quantity = m.Quantity,
                    Price = m.Price,
                    Warranty = m.Warranty,
                    DisplayTechnology = m.DisplayTechnology == null ? null : m.DisplayTechnology.Name,
                    Color = m.Color == null ? null : m.Color.Name,
                    ImageUrl = m.ImageUrl,
                    Seller = m.Seller,
                })
                .FirstOrDefaultAsync();
            
            this.guard.AgainstProductThatIsNull<TelevisionEditViewModel>(televisionExport, ErrorMessageForInvalidProductId);
            
            return televisionExport;
        }
        
        public async Task<IEnumerable<TelevisionDetailsExportViewModel>> GetUserTelevisionsAsync(string userId)
        {
            var client = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

            this.guard.AgainstInvalidUserId<Client>(client, ErrorMessageForInvalidUserId);

            var userTelevisions = await this.GetTelevisionsAsTelevisionsDetailsExportViewModelsAsync<Television>(m => m.SellerId == client.Id);

            return userTelevisions;
        }

        public async Task MarkTelevisionAsBoughtAsync(int id)
        {
	        var television = await this.repository.GetByIdAsync<Television>(id);
	        
	        this.guard.AgainstProductThatIsNull<Television>(television, ErrorMessageForInvalidProductId);
	        
	        this.guard.AgainstProductThatIsDeleted(television.IsDeleted, ErrorMessageForDeletedProduct);
	        
	        this.guard.AgainstProductThatIsOutOfStock(television.Quantity, ErrorMessageForProductThatIsOutOfStock);
	        
	        television.Quantity--;
	        
	        await this.repository.SaveChangesAsync();
		}

		private async Task<IList<TelevisionDetailsExportViewModel>>
            GetTelevisionsAsTelevisionsDetailsExportViewModelsAsync<T>(Expression<Func<Television, bool>> condition)
        {
            var televisionsAsTelevisionsExportViewModels = await this.repository
                .AllAsReadOnly<Television>(m => !m.IsDeleted)
                .Where(condition)
                .Select(m => new TelevisionDetailsExportViewModel()
                {
                    Id = m.Id,
                    Brand = m.Brand.Name,
                    Price = m.Price,
                    Warranty = m.Warranty,
                    DisplaySize = m.DisplaySize.Value,
                    DisplayTechnology = m.DisplayTechnology != null
                        ? m.DisplayTechnology.Name
                        : UnknownCharacteristic,
                    Resolution = m.Resolution.Value,
                    Type = m.Type.Name,
                    Color = m.Color != null ? m.Color.Name : UnknownCharacteristic,
                    ImageUrl = m.ImageUrl,
                    AddedOn = m.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture),
                    Quantity = m.Quantity,
                    Seller = m.Seller,
                    SellerFirstName = m.Seller != null ? m.Seller.User.FirstName : null,
                    SellerLastName = m.Seller != null ? m.Seller.User.LastName : null,
                })
                .ToListAsync();

            return televisionsAsTelevisionsExportViewModels;
        }

        public async Task<int> AddTelevisionAsync(TelevisionImportViewModel model, string? userId)
        {
	        var television = new Television()
	        {
		        ImageUrl = model.ImageUrl,
		        Warranty = model.Warranty,
		        Price = model.Price,
		        Quantity = model.Quantity,
		        IsDeleted = false,
		        AddedOn = DateTime.UtcNow.Date,
	        };
	        Client? dbClient = null;

	        if (userId != null)
	        {
		        dbClient = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

                this.guard.AgainstInvalidUserId<Client>(dbClient, ErrorMessageForInvalidUserId);
            }

	        television.Seller = dbClient;

	        television = await this.SetNavigationPropertiesAsync(
                television,
                model.Brand,
                model.DisplaySize,
                model.Resolution,
                model.Type,
                model.DisplayTechnology,
                model.Color);
	        
            await this.repository.AddAsync<Television>(television);
	        
            await this.repository.SaveChangesAsync();
	        
            return television.Id;
        }

		public async Task DeleteTelevisionAsync(int id)
        {
	        var television = await this.repository.GetByIdAsync<Television>(id);
	        this.guard.AgainstProductThatIsNull<Television>(television, ErrorMessageForInvalidProductId);
	        this.guard.AgainstProductThatIsDeleted(television.IsDeleted, ErrorMessageForDeletedProduct);
	        television.IsDeleted = true;
	        await this.repository.SaveChangesAsync();
		}

		private async Task<Television> SetNavigationPropertiesAsync(
            Television television,
            string brand,
            double displaySize,
            string resolution,
            string type,
            string? displayTechnology,
            string? color)

		{
			var brandNormalized = brand.ToLower();
			var dbBrand = await this.repository.GetByPropertyAsync<Brand>(b => EF.Functions.Like(b.Name.ToLower(), brandNormalized));
			dbBrand ??= new Brand { Name = brand };
			television.Brand = dbBrand;

			var dbDisplaySize = await this.repository.GetByPropertyAsync<DisplaySize>(ds => ds.Value == displaySize);
			dbDisplaySize ??= new DisplaySize { Value = displaySize };
			television.DisplaySize = dbDisplaySize;

			var resolutionNormalized = resolution.ToLower();
			var dbResolution = await this.repository.GetByPropertyAsync<Resolution>(r => EF.Functions.Like(r.Value.ToLower(), resolutionNormalized));
			dbResolution ??= new Resolution { Value = resolution };
            television.Resolution = dbResolution;

            var typeNormalized = type.ToLower();
            var dbType = await this.repository.GetByPropertyAsync<Type>(t => EF.Functions.Like(t.Name.ToLower(), typeNormalized));
            dbType ??= new Type { Name = type };
            television.Type = dbType;

            if (String.IsNullOrWhiteSpace(displayTechnology))
            {
				television.DisplayTechnology = null;
			}

            else
            {
	            var displayTechnologyNormalized = displayTechnology.ToLower();
	            var dbDisplayTechnology = await this.repository.GetByPropertyAsync<DisplayTechnology>(dt => EF.Functions.Like(dt.Name.ToLower(), displayTechnologyNormalized));
	            dbDisplayTechnology ??= new DisplayTechnology { Name = displayTechnology };
	            television.DisplayTechnology = dbDisplayTechnology;
			}

            if (String.IsNullOrWhiteSpace(color))
            {
	            television.Color = null;
            }

            else
            {
	            var colorNormalized = color.ToLower();
	            var dbColor = await this.repository.GetByPropertyAsync<Color>(c => EF.Functions.Like(c.Name.ToLower(), colorNormalized));
	            dbColor ??= new Color { Name = color };
	            television.Color = dbColor;
			}

            return television;
		}
	}
}
