﻿using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Models.Keyboard;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using TechStore.Core.Exceptions;
using System.Linq.Expressions;
using System.Globalization;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Core.Services
{
    public class KeyboardService : IKeyboardService
    {
        private readonly IRepository repository;
        private readonly IGuard guard;

        public KeyboardService(IRepository repository, IGuard guard)
        {
            this.repository = repository;
            this.guard = guard;
        }

        public async Task<int> AddKeyboardAsync(KeyboardImportViewModel model, string? userId)
        {
            var keyboard = new Keyboard()
            {
                ImageUrl = model.ImageUrl,
                Warranty = model.Warranty,
                Price = model.Price != null ? model.Price.Value : default,
                Quantity = model.Quantity != null ? model.Quantity.Value : default,
                IsWireless = model.IsWireless != null ? model.IsWireless.Value : default,

                IsDeleted = false,
                AddedOn = DateTime.UtcNow.Date,
            };

            Client? dbClient = null;

            if (userId == null)
            {
                dbClient = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

                this.guard.AgainstInvalidUserId<Client>(dbClient, ErrorMessageForInvalidUserId);
            }

            keyboard.Seller = dbClient;

            keyboard = await this.SetNavigationPropertiesAsync(keyboard, model.Brand, model.Type, model.Format, model.Color);

            await this.repository.AddAsync<Keyboard>(keyboard);

            await this.repository.SaveChangesAsync();

            return keyboard.Id;
        }

        public async Task<int> EditKeyboardAsync(KeyboardEditViewModel model)
        {
	        var keyboard = await this.repository
		        .All<Keyboard>(k => !k.IsDeleted)
		        .Where(k => k.Id == model.Id)
		        .Include(k => k.Brand)
		        .Include(k => k.Format)
		        .Include(k => k.Type)
		        .Include(k => k.Color)
		        .FirstOrDefaultAsync();

	        this.guard.AgainstProductThatIsNull<Keyboard>(keyboard, ErrorMessageForInvalidProductId);

	        keyboard.ImageUrl = model.ImageUrl;
	        keyboard.Warranty = model.Warranty;
	        keyboard.Price = model.Price != null ? model.Price.Value : default;
	        keyboard.Quantity = model.Quantity != null ? model.Quantity.Value : default;
	        keyboard.IsWireless = model.IsWireless != null ? model.IsWireless.Value : default;
	        keyboard.AddedOn = DateTime.UtcNow.Date;

	        keyboard = await this.SetNavigationPropertiesAsync(
		        keyboard,
		        model.Brand,
		        model.Type,
		        model.Format,
		        model.Color);

	        await this.repository.SaveChangesAsync();

            return keyboard.Id;
		}

		public async Task<KeyboardsQueryModel> GetAllKeyboardsAsync(string? format = null, string? type = null,
            Wireless wireless = Wireless.Regardless, string? keyword = null, Sorting sorting = Sorting.Newest,
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
                    IsWireless = k.IsWireless,
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

        public async Task<KeyboardDetailsExportViewModel> GetKeyboardByIdAsKeyboardDetailsExportViewModelAsync(int id)
        {
            var keyboardExports = await this.GetKeyboardsAsKeyboardDetailsExportViewModelsAsync<Keyboard>(k => k.Id == id);
            
            this.guard.AgainstNullOrEmptyCollection<KeyboardDetailsExportViewModel>(keyboardExports, ErrorMessageForInvalidProductId);
            
            return keyboardExports.First();
        }

        private async Task<IList<KeyboardDetailsExportViewModel>> GetKeyboardsAsKeyboardDetailsExportViewModelsAsync<T>(
            Expression<Func<Keyboard, bool>> condition)
        {
            var keyboardsAsKeyboardDetailsExportViewModels = await this.repository
                .AllAsReadOnly<Keyboard>(k => !k.IsDeleted)
                .Where(condition)
                .Select(k => new KeyboardDetailsExportViewModel()
                {
                    Id = k.Id,
                    Brand = k.Brand.Name,
                    Price = k.Price,
                    IsWireless = k.IsWireless,
                    Format = k.Format != null ? k.Format.Name : UnknownCharacteristic,
                    Type = k.Type.Name,
                    Color = k.Color != null ? k.Color.Name : UnknownCharacteristic,
                    ImageUrl = k.ImageUrl,
                    Warranty = k.Warranty,
                    Quantity = k.Quantity,
                    AddedOn = k.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture),
                    Seller = k.Seller,
                    SellerFirstName = k.Seller != null ? k.Seller.User.FirstName : null,
                    SellerLastName = k.Seller != null ? k.Seller.User.LastName : null,
                })
                .ToListAsync();

            return keyboardsAsKeyboardDetailsExportViewModels;
        }

        public async Task DeleteKeyboardAsync(int id)
        {
            var keyboard = await this.repository.GetByIdAsync<Keyboard>(id);
            
            this.guard.AgainstProductThatIsNull<Keyboard>(keyboard, ErrorMessageForInvalidProductId);
            
            this.guard.AgainstProductThatIsDeleted(keyboard.IsDeleted, ErrorMessageForDeletedProduct);
            
            keyboard.IsDeleted = true;
            
            await this.repository.SaveChangesAsync();
        }

        public async Task<KeyboardEditViewModel> GetKeyboardByIdAsKeyboardEditViewModelAsync(int id)
        {
	        var keyboardExport = await this.repository
		        .AllAsReadOnly<Keyboard>(k => !k.IsDeleted)
		        .Where(k => k.Id == id)
		        .Select(k => new KeyboardEditViewModel()
		        {
			        Id = k.Id,
			        Brand = k.Brand.Name,
			        IsWireless = k.IsWireless,
			        Type = k.Type.Name,
			        Quantity = k.Quantity,
			        Price = k.Price,
			        Warranty = k.Warranty,
			        Format = k.Format == null ? null : k.Format.Name,
			        Color = k.Color == null ? null : k.Color.Name,
			        ImageUrl = k.ImageUrl,
			        Seller = k.Seller,
		        })
		        .FirstOrDefaultAsync();

	        this.guard.AgainstProductThatIsNull<KeyboardEditViewModel>(keyboardExport, ErrorMessageForInvalidProductId);

	        return keyboardExport;
		}

        public async Task<IEnumerable<KeyboardDetailsExportViewModel>> GetUserKeyboardsAsync(string userId)
        {
	        var client = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

	        this.guard.AgainstInvalidUserId<Client>(client, ErrorMessageForInvalidUserId);

	        var userKeyboards = await this.GetKeyboardsAsKeyboardDetailsExportViewModelsAsync<Keyboard>(k => k.SellerId == client.Id);

	        return userKeyboards;
		}

        public async Task MarkKeyboardAsBoughtAsync(int id)
        {
	        var keyboard = await this.repository.GetByIdAsync<Keyboard>(id);

	        this.guard.AgainstProductThatIsNull<Keyboard>(keyboard, ErrorMessageForInvalidProductId);

	        this.guard.AgainstProductThatIsDeleted(keyboard.IsDeleted, ErrorMessageForDeletedProduct);

	        this.guard.AgainstProductThatIsOutOfStock(keyboard.Quantity, ErrorMessageForProductThatIsOutOfStock);

	        keyboard.Quantity--;

	        await this.repository.SaveChangesAsync();
		}

		private async Task<Keyboard> SetNavigationPropertiesAsync(Keyboard keyboard, string brand, string type,
            string? format, string? color)
        {
            var brandNormalized = brand.ToLower();
            var dbBrand = await this.repository.GetByPropertyAsync<Brand>(b => EF.Functions.Like(b.Name.ToLower(), brandNormalized));
            dbBrand ??= new Brand { Name = brand };
            keyboard.Brand = dbBrand;

            var typeNormalized = type.ToLower();
            var dbType = await this.repository.GetByPropertyAsync<Type>(t => EF.Functions.Like(t.Name.ToLower(), typeNormalized));
            dbType ??= new Type { Name = type };
            keyboard.Type = dbType;

            if (String.IsNullOrEmpty(format))
            {
                keyboard.Format = null;
            }
            else
            {
                var formatNormalized = format.ToLower();
                var dbFormat = await this.repository.GetByPropertyAsync<Format>(f => EF.Functions.Like(f.Name.ToLower(), formatNormalized));
                dbFormat ??= new Format { Name = format };
                keyboard.Format = dbFormat;
            }

            if (String.IsNullOrWhiteSpace(color))
            {
                keyboard.Color = null;
            }
            else
            {
                var colorNormalized = color.ToLower();
                var dbColor = await this.repository.GetByPropertyAsync<Color>(c => EF.Functions.Like(c.Name.ToLower(), colorNormalized));
                dbColor ??= new Color { Name = color };
                keyboard.Color = dbColor;
            }

            return keyboard;
        }
    }
}
