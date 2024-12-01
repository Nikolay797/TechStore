using System.ComponentModel.DataAnnotations;
using TechStore.Core.Contracts;
using static TechStore.Infrastructure.Constants.DataConstant.BrandConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ColorConstants;

namespace TechStore.Core.Models.Product
{
	public class ProductImportViewModel : IProductModel
	{
		[Display(Name = "Brand")]
		[Required]
		[StringLength(BrandNameMaxLength, MinimumLength = BrandNameMinLength)]
		public string Brand { get; init; } = null!;

		[Display(Name = "Quantity")]
		[Required]
		[Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = IntegerErrorMessage)]
		public int? Quantity { get; init; }

		[Display(Name = "Price")]
		[Required]
		public decimal? Price { get; init; }

		[Display(Name = "Warranty")]
		[Required]
		[Range(WarrantyMinValue, WarrantyMaxValue, ErrorMessage = IntegerErrorMessage)]
		public int Warranty { get; init; }

		[Display(Name = "Color")]
		[StringLength(ColorNameMaxLength, MinimumLength = ColorNameMinLength)]
		public string? Color { get; init; }

		[Display(Name = "Image Url")]
		[Url]
		public string? ImageUrl { get; init; }
	}
}
