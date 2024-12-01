using System.ComponentModel.DataAnnotations;
using TechStore.Core.Models.Product;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;

namespace TechStore.Core.Models.Mice
{
	public class MouseImportViewModel : ProductImportViewModel
	{
		public MouseImportViewModel()
		{
			this.Sensitivities = Enumerable.Empty<string>();
		}

		[Display(Name = "Wireless")]
		public bool? IsWireless { get; set; }

		[Display(Name = "Type")]
		[Required]
		[StringLength(TypeNameMaxLength, MinimumLength = TypeNameMinLength)]
		public string Type { get; init; } = null!;

		[Display(Name = "Sensitivity Range")]
		public string? Sensitivity { get; set; }

		public IEnumerable<string> Sensitivities { get; set; }
	}
}
