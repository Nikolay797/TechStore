using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.BrandConstants;
using static TechStore.Infrastructure.Constants.DataConstant.DisplayTechnologyConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ResolutionConstants;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ColorConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;

namespace TechStore.Core.Models.Television
{
	public class TelevisionImportViewModel
	{
		[Display(Name = "Brand")]
		[Required]
		[StringLength(BrandNameMaxLength, MinimumLength = BrandNameMinLength)]
		public string Brand { get; init; } = null!;
		
		[Display(Name = "Display Size")]
		[Required]
		public double DisplaySize { get; init; }

		[Display(Name = "Resolution")]
		[Required]
		[StringLength(ResolutionValueMaxLength, MinimumLength = ResolutionValueMinLength)]
		public string Resolution { get; init; } = null!;

		[Display(Name = "Type")]
		[Required]
		[StringLength(TypeNameMaxLength, MinimumLength = TypeNameMinLength)]
		public string Type { get; init; } = null!;

		[Display(Name = "Quantity")]
		[Required]
		[Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = IntegerErrorMessage)]
		public int Quantity { get; init; }

		[Display(Name = "Price")]
		[Required]
		public decimal Price { get; init; }

		[Display(Name = "Warranty")]
		[Range(WarrantyMinValue, WarrantyMaxValue, ErrorMessage = IntegerErrorMessage)]
		public int Warranty { get; init; }

		[Display(Name = "Display Technology")]
		[StringLength(DisplayTechnologyNameMaxLength, MinimumLength = DisplayTechnologyNameMinLength)]
		public string? DisplayTechnology { get; init; }

		[Display(Name = "Color")]
		[StringLength(ColorNameMaxLength, MinimumLength = ColorNameMinLength)]
		public string? Color { get; init; }

		[Display(Name = "Image Url")]
		[Url]
		public string? ImageUrl { get; init; }
	}
}
