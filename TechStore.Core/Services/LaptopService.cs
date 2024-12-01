using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TechStore.Core.Contracts;
using TechStore.Core.Models.Laptop;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using System.Linq.Expressions;
using TechStore.Core.Exceptions;
using TechStore.Core.Enums;


namespace TechStore.Core.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly IRepository repository;
        private readonly IGuard guard;

        public LaptopService(IRepository repository, IGuard guard)
        {
            this.repository = repository;
            this.guard = guard;
        }

        public async Task<int> AddLaptopAsync(LaptopImportViewModel model, string? userId)
        {
            var laptop = new Laptop()
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

            laptop.Seller = dbClient;

            laptop = await this.SetNavigationPropertiesAsync(
                laptop,
                model.Brand,
                model.CPU,
                model.RAM,
                model.SSDCapacity,
                model.VideoCard,
                model.Type,
                model.DisplaySize,
                model.DisplayCoverage,
                model.DisplayTechnology,
                model.Resolution,
                model.Color);

            await this.repository.AddAsync<Laptop>(laptop);
            await this.repository.SaveChangesAsync();

            return laptop.Id;
        }


        public async Task<LaptopsQueryModel> GetAllLaptopsAsync(string? cpu = null, int? ram = null, int? ssdCapacity = null, string? videoCard = null, string? keyword = null, Sorting sorting = Sorting.Newest, int currentPage = 1)
        {
            var result = new LaptopsQueryModel();

            var query = this.repository.AllAsReadOnly<Laptop>(l => !l.IsDeleted);

			if (!String.IsNullOrEmpty(cpu))
			{
				query = query.Where(l => l.CPU.Name == cpu);
			}

			if (ram is not null)
			{
				query = query.Where(l => l.RAM.Value == ram);
			}

			if (ssdCapacity is not null)
			{
				query = query.Where(l => l.SSDCapacity.Value == ssdCapacity);
			}

            if (!String.IsNullOrEmpty(videoCard))
            {
	            query = query.Where(l => l.VideoCard.Name == videoCard);
			}

            if (!String.IsNullOrEmpty(keyword))
            {
				var searchTerm = $"%{keyword.ToLower()}%";

				query = query.Where(l => EF.Functions.Like(l.Brand.Name.ToLower(), searchTerm)
				                         || EF.Functions.Like(l.CPU.Name.ToLower(), searchTerm)
				                         || EF.Functions.Like(l.VideoCard.Name.ToLower(), searchTerm)
				                         || EF.Functions.Like(l.Type.Name.ToLower(), searchTerm));
			}

            query = sorting switch
            {
	            Sorting.Brand => query.OrderBy(l => l.Brand.Name),
	            Sorting.PriceMinToMax => query.OrderBy(l => l.Price),
	            Sorting.PriceMaxToMin => query.OrderByDescending(l => l.Price),
	            _ => query.OrderByDescending(l => l.Id)
            };

            result.Laptops = await query
                .Skip((currentPage - 1) * ProductsPerPage)
                .Take(ProductsPerPage)
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

            result.TotalLaptopsCount = await query.CountAsync();
            
            return result;
        }

        public async Task<LaptopDetailsExportViewModel> GetLaptopByIdAsLaptopDetailsExportViewModelAsync(int id)
        {
            var laptopExport = await this.GetLaptopsAsLaptopDetailsExportViewModelsAsync<Laptop>(l => l.Id == id);

            if (laptopExport is null)
            {
                throw new ArgumentException(ErrorMessageForInvalidProductId);
            }

            return laptopExport.First();
        }

        public async Task DeleteLaptopAsync(int id)
        {
			var laptop = await this.repository.GetByIdAsync<Laptop>(id);

			this.guard.AgainstProductThatIsNull<Laptop>(laptop, ErrorMessageForInvalidProductId);
			
			this.guard.AgainstProductThatIsDeleted(laptop.IsDeleted, ErrorMessageForDeletedProduct);

			laptop.IsDeleted = true;

            await this.repository.SaveChangesAsync();
        }

        public async Task<int> EditLaptopAsync(LaptopEditViewModel model)
        {
            var laptop = await this.repository
                .All<Laptop>(l => !l.IsDeleted)
                .Where(l => l.Id == model.Id)
                .Include(l => l.Brand)
                .Include(l => l.CPU)
                .Include(l => l.RAM)
                .Include(l => l.SSDCapacity)
                .Include(l => l.VideoCard)
                .Include(l => l.Type)
                .Include(l => l.DisplaySize)
                .Include(l => l.DisplayCoverage)
                .Include(l => l.DisplayTechnology)
                .Include(l => l.Resolution)
                .Include(l => l.Color)
                .FirstOrDefaultAsync();

			this.guard.AgainstProductThatIsNull<Laptop>(laptop, ErrorMessageForInvalidProductId);

			laptop.ImageUrl = model.ImageUrl;
            laptop.Warranty = model.Warranty;
            laptop.Price = model.Price != null ? model.Price.Value : default;
            laptop.Quantity = model.Quantity != null ? model.Quantity.Value : default;
            laptop.AddedOn = DateTime.UtcNow.Date;

            laptop = await this.SetNavigationPropertiesAsync(laptop, model.Brand, model.CPU, model.RAM, model.SSDCapacity, model.VideoCard, model.Type, model.DisplaySize, model.DisplayCoverage, model.DisplayTechnology, model.Resolution, model.Color);

            await this.repository.SaveChangesAsync();
            return laptop.Id;
        }
        public async Task<LaptopEditViewModel> GetLaptopByIdAsLaptopEditViewModelAsync(int id)
        {
            var laptopExport = await this.repository
                .All<Laptop>(l => !l.IsDeleted)
                .Where(l => l.Id == id)
                .Select(l => new LaptopEditViewModel()
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
                    DisplayCoverage = l.DisplayCoverage == null ? null : l.DisplayCoverage.Name,
                    DisplayTechnology = l.DisplayTechnology == null ? null : l.DisplayTechnology.Name,
                    Resolution = l.Resolution == null ? null : l.Resolution.Value,
                    Color = l.Color == null ? null : l.Color.Name,
                    ImageUrl = l.ImageUrl,
                    Quantity = l.Quantity,
                    Seller = l.Seller,
                })
                .FirstOrDefaultAsync();

			this.guard.AgainstProductThatIsNull<LaptopEditViewModel>(laptopExport, ErrorMessageForInvalidProductId);
			
			return laptopExport;
        }

        public async Task<IEnumerable<LaptopDetailsExportViewModel>> GetUserLaptopsAsync(string userId)
        {
            var client = await this.repository.GetByPropertyAsync<Client>(c => c.UserId == userId);

            this.guard.AgainstInvalidUserId<Client>(client, ErrorMessageForInvalidUserId);

            var userLaptops = await this.GetLaptopsAsLaptopDetailsExportViewModelsAsync<Laptop>(l => l.SellerId == client.Id);

            return userLaptops;
        }

        public async Task MarkLaptopAsBoughtAsync(int id)
        {
	        var laptop = await this.repository.GetByIdAsync<Laptop>(id);
	        
	        this.guard.AgainstProductThatIsNull<Laptop>(laptop, ErrorMessageForInvalidProductId);
	        
	        this.guard.AgainstProductThatIsDeleted(laptop.IsDeleted, ErrorMessageForDeletedProduct);
	        
	        this.guard.AgainstProductThatIsOutOfStock(laptop.Quantity, ErrorMessageForProductThatIsOutOfStock);
	        
	        laptop.Quantity--;
	        
	        await this.repository.SaveChangesAsync();
		}

        public async Task<IEnumerable<string>> GetAllCpusNamesAsync()
        {
			return await this.repository.AllAsReadOnly<Laptop>()
				.Select(l => l.CPU.Name)
				.Distinct()
				.OrderBy(n => n)
                .ToListAsync();
		}

        public async Task<IEnumerable<int>> GetAllRamsValuesAsync()
        {
			return await this.repository.AllAsReadOnly<Laptop>()
				.Select(l => l.RAM.Value)
				.Distinct()
				.OrderBy(v => v)
                .ToListAsync();
		}

        public async Task<IEnumerable<int>> GetAllSsdCapacitiesValuesAsync()
        {
			return await this.repository.AllAsReadOnly<Laptop>()
				.Select(l => l.SSDCapacity.Value)
				.Distinct()
				.OrderBy(v => v)
                .ToListAsync();
		}

        public async Task<IEnumerable<string>> GetAllVideoCardsNamesAsync()
        {
			return await this.repository.AllAsReadOnly<Laptop>()
				.Select(l => l.VideoCard.Name)
				.Distinct()
				.OrderBy(n => n)
                .ToListAsync();
		}

        private async Task<Laptop> SetNavigationPropertiesAsync(
            Laptop laptop,
            string brand,
            string cpu,
            int ram,
            int ssdCapacity,
            string videoCard,
            string type,
            double displaySize,
            string? displayCoverage,
            string? displayTechnology,
            string? resolution,
            string? color)

        {
            var brandNormalized = brand.ToLower();
            var dbBrand = await this.repository.GetByPropertyAsync<Brand>(b => EF.Functions.Like(b.Name.ToLower(), brandNormalized));
            dbBrand ??= new Brand { Name = brand };
            laptop.Brand = dbBrand;
            
            var cpuNormalized = cpu.ToLower();
            var dbCpu = await this.repository.GetByPropertyAsync<CPU>(c => EF.Functions.Like(c.Name.ToLower(), cpuNormalized));
            dbCpu ??= new CPU { Name = cpu };
            laptop.CPU = dbCpu;
            
            var dbRam = await this.repository.GetByPropertyAsync<RAM>(r => r.Value == ram);
            dbRam ??= new RAM { Value = ram };
            laptop.RAM = dbRam;
            
            var dbSsdCapacity = await this.repository.GetByPropertyAsync<SSDCapacity>(s => s.Value == ssdCapacity);
            dbSsdCapacity ??= new SSDCapacity { Value = ssdCapacity };
            laptop.SSDCapacity = dbSsdCapacity;

            var videoCardNormalized = videoCard.ToLower();
            var dbVideoCard = await this.repository.GetByPropertyAsync<VideoCard>(vc => EF.Functions.Like(vc.Name.ToLower(), videoCardNormalized));
            dbVideoCard ??= new VideoCard { Name = videoCard };
            laptop.VideoCard = dbVideoCard;

            var typeNormalized = type.ToLower();
            var dbType = await this.repository.GetByPropertyAsync<Type>(t => EF.Functions.Like(t.Name.ToLower(), typeNormalized));
            dbType ??= new Type { Name = type };
            laptop.Type = dbType;
            
            var dbDisplaySize = await this.repository.GetByPropertyAsync<DisplaySize>(ds => ds.Value == displaySize);
            dbDisplaySize ??= new DisplaySize { Value = displaySize };
            laptop.DisplaySize = dbDisplaySize;

            if (String.IsNullOrWhiteSpace(displayCoverage))
            {
                laptop.DisplayCoverage = null;
            }
            else
            {
                var displayCoverageNormalized = displayCoverage.ToLower();
                var dbDisplayCoverage = await this.repository.GetByPropertyAsync<DisplayCoverage>(dc => EF.Functions.Like(dc.Name.ToLower(), displayCoverageNormalized));
                dbDisplayCoverage ??= new DisplayCoverage { Name = displayCoverage };
                laptop.DisplayCoverage = dbDisplayCoverage;
            }

            if (String.IsNullOrWhiteSpace(displayTechnology))
            {
                laptop.DisplayTechnology = null;
            }
            else
            {
                var displayTechnologyNormalized = displayTechnology.ToLower();
                var dbDisplayTechnology = await this.repository.GetByPropertyAsync<DisplayTechnology>(dt => EF.Functions.Like(dt.Name.ToLower(), displayTechnologyNormalized));
                dbDisplayTechnology ??= new DisplayTechnology { Name = displayTechnology };
                laptop.DisplayTechnology = dbDisplayTechnology;
            }
            
            if (String.IsNullOrWhiteSpace(resolution))
            {
                laptop.Resolution = null;
            }
            else
            {
                var resolutionNormalized = resolution.ToLower();
                var dbResolution = await this.repository.GetByPropertyAsync<Resolution>(r => EF.Functions.Like(r.Value.ToLower(), resolutionNormalized));
                dbResolution ??= new Resolution { Value = resolution };
                laptop.Resolution = dbResolution;
            }

            if (String.IsNullOrWhiteSpace(color))
            {
                laptop.Color = null;
            }
            else
            {
                var colorNormalized = color.ToLower();
                var dbColor = await this.repository.GetByPropertyAsync<Color>(c => EF.Functions.Like(c.Name.ToLower(), colorNormalized));
                dbColor ??= new Color { Name = color };
                laptop.Color = dbColor;
            }

            return laptop;
        }

        private async Task<IList<LaptopDetailsExportViewModel>> GetLaptopsAsLaptopDetailsExportViewModelsAsync<T>(
            Expression<Func<Laptop, bool>> condition)
        {
            var laptopsAsLaptopDetailsExportViewModels = await this.repository
                .AllAsReadOnly<Laptop>(l => !l.IsDeleted)
				.Where(condition)
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
                    DisplayCoverage = l.DisplayCoverage != null ? l.DisplayCoverage.Name : UnknownCharacteristic,
                    DisplayTechnology = l.DisplayTechnology != null ? l.DisplayTechnology.Name : UnknownCharacteristic,
                    Resolution = l.Resolution != null ? l.Resolution.Value : UnknownCharacteristic,
                    Color = l.Color != null ? l.Color.Name : UnknownCharacteristic,
                    ImageUrl = l.ImageUrl,
                    AddedOn = l.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture),
                    Quantity = l.Quantity,
                    Seller = l.Seller,
                    SellerFirstName = l.Seller == null ? null : l.Seller.User.FirstName,
                    SellerLastName = l.Seller == null ? null : l.Seller.User.LastName,
                })
                .ToListAsync();
            
            return laptopsAsLaptopDetailsExportViewModels;
        }
    }
}