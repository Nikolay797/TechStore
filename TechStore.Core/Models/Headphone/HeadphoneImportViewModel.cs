using System.ComponentModel.DataAnnotations;
using TechStore.Core.Models.Product;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;

namespace TechStore.Core.Models.Headphone
{
	public class HeadphoneImportViewModel : ProductImportViewModel
	{
		[Display(Name = "Type")]
		[Required]
		[StringLength(TypeNameMaxLength, MinimumLength = TypeNameMinLength)]
		public string Type { get; init; } = null!;

		[Display(Name = "Wireless")]
		public bool? IsWireless { get; set; }

		[Display(Name = "Microphone")]
		public bool? HasMicrophone { get; set; }
	}
}
