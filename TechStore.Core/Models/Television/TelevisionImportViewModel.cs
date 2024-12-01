using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.DisplayTechnologyConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ResolutionConstants;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Television
{
	public class TelevisionImportViewModel : ProductImportViewModel
	{
		
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


		[Display(Name = "Display Technology")]
		[StringLength(DisplayTechnologyNameMaxLength, MinimumLength = DisplayTechnologyNameMinLength)]
		public string? DisplayTechnology { get; init; }
	}
}
