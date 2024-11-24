using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.BrandConstants;
using static TechStore.Infrastructure.Constants.DataConstant.CPUConstants;
using static TechStore.Infrastructure.Constants.DataConstant.LaptopConstants;
using static TechStore.Infrastructure.Constants.DataConstant.DisplayCoverageConstants;
using static TechStore.Infrastructure.Constants.DataConstant.DisplayTechnologyConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ResolutionConstants;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;
using static TechStore.Infrastructure.Constants.DataConstant.VideoCardConstants;
using static TechStore.Infrastructure.Constants.DataConstant.ColorConstants;


namespace TechStore.Core.Models.Laptop
{
    public class LaptopImportViewModel
    {
        [Display(Name = "Brand")]
        [Required]
        [StringLength(BrandNameMaxLength, MinimumLength = BrandNameMinLength)]
        public string Brand { get; init; } = null!;
        
        [Display(Name = "CPU")]
        [Required]
        [StringLength(CPUNameMaxLength, MinimumLength = CPUNameMinLength)]
        public string CPU { get; init; } = null!;

        [Display(Name = "RAM")]
        [Required]
        [Range(RAMMinValue, RAMMaxValue, ErrorMessage = IntegerErrorMessage)]
        public int RAM { get; init; }

        [Display(Name = "SSD Capacity")]
        [Required]
        [Range(SSDCapacityMinValue, SSDCapacityMaxValue, ErrorMessage = IntegerErrorMessage)]
        public int SSDCapacity { get; init; }

        [Display(Name = "Video Card")]
        [Required]
        [StringLength(VideoCardNameMaxLength, MinimumLength = VideoCardNameMinLength)]
        public string VideoCard { get; init; } = null!;

        [Display(Name = "Display Size")]
        [Required]
        public double DisplaySize { get; init; }

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

        [Display(Name = "Display Coverage")]
        [StringLength(DisplayCoverageNameMaxLength, MinimumLength = DisplayCoverageNameMinLength)]
        public string? DisplayCoverage { get; init; }

        [Display(Name = "Display Technology")]
        [StringLength(DisplayTechnologyNameMaxLength, MinimumLength = DisplayTechnologyNameMinLength)]
        public string? DisplayTechnology { get; init; }

        [Display(Name = "Resolution")]
        [StringLength(ResolutionValueMaxLength, MinimumLength = ResolutionValueMinLength)]
        public string? Resolution { get; init; }

        [Display(Name = "Color")]
        [StringLength(ColorNameMaxLength, MinimumLength = ColorNameMinLength)]
        public string? Color { get; init; }

        [Display(Name = "Image Url")]
        [Url]
        public string? ImageUrl { get; init; }
    }
}
