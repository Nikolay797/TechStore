using Microsoft.EntityFrameworkCore;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Headphone;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.GlobalConstants;
using System.Linq.Expressions;
using System.Globalization;

namespace TechStore.Core.Services
{
	public class HeadphoneService : IHeadphoneService
	{
		private readonly IRepository repository;
		private readonly IGuard guard;

		public HeadphoneService(IRepository repository, IGuard guard)
		{
			this.repository = repository;
			this.guard = guard;
		}

		public async Task DeleteHeadphoneAsync(int id)
		{
			var headphone = await this.repository.GetByIdAsync<Headphone>(id);

			this.guard.AgainstProductThatIsNull<Headphone>(headphone, ErrorMessageForInvalidProductId);

			this.guard.AgainstProductThatIsDeleted(headphone.IsDeleted, ErrorMessageForDeletedProduct);

			headphone.IsDeleted = true;

			await this.repository.SaveChangesAsync();
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

		public async Task<HeadphoneDetailsExportViewModel>
			GetHeadphoneByIdAsHeadphoneDetailsExportViewModelAsync(int id)
		{
			var headphoneExports = await this.GetHeadphonesAsHeadphonesDetailsExportViewModelsAsync<Headphone>(h => h.Id == id);

			this.guard.AgainstNullOrEmptyCollection<HeadphoneDetailsExportViewModel>(headphoneExports, ErrorMessageForInvalidProductId);

			return headphoneExports.First();
		}

		private async Task<IList<HeadphoneDetailsExportViewModel>>
			GetHeadphonesAsHeadphonesDetailsExportViewModelsAsync<T>(Expression<Func<Headphone, bool>> condition)
		{
			var headphonesAsHeadphoneDetailsExportViewModels = await this.repository
				.AllAsReadOnly<Headphone>(h => !h.IsDeleted)
				.Where(condition)
				.Select(h => new HeadphoneDetailsExportViewModel()
				{
					Id = h.Id,
					Brand = h.Brand.Name,
					Price = h.Price,
					IsWireless = h.IsWireless,
					HasMicrophone = h.HasMicrophone,
					Type = h.Type.Name,
					Color = h.Color != null ? h.Color.Name : UnknownCharacteristic,
					ImageUrl = h.ImageUrl,
					Warranty = h.Warranty,
					Quantity = h.Quantity,
					AddedOn = h.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture),
					Seller = h.Seller,
					SellerFirstName = h.Seller != null ? h.Seller.User.FirstName : null,
					SellerLastName = h.Seller != null ? h.Seller.User.LastName : null,
				})
				.ToListAsync();

			return headphonesAsHeadphoneDetailsExportViewModels;
		}
	}
}
