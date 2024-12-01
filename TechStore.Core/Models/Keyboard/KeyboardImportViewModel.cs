using System.ComponentModel.DataAnnotations;
using TechStore.Core.Models.Product;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;

namespace TechStore.Core.Models.Keyboard
{
    public class KeyboardImportViewModel : ProductImportViewModel
	{
	    
        [Display(Name = "Wireless")]
		public bool? IsWireless { get; set; }

		[Display(Name = "Type")]
        [Required]
        [StringLength(TypeNameMaxLength, MinimumLength = TypeNameMinLength)]
        public string Type { get; init; } = null!;


        [Display(Name = "Format")]
        public string? Format { get; init; }

    }
}
