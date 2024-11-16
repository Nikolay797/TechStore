using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.Arm;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Infrastructure.Data.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CPU))]
        public int CPUId { get; set; }
        public virtual CPU CPU { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(RAM))]
        public int RAMId { get; set; }
        public virtual RAM RAM { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(SSDCapacity))]
        public int SSDCapacityId { get; set; }
        public virtual SSDCapacity SSDCapacity { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(VideoCard))]
        public int VideoCardId { get; set; }
        public virtual VideoCard VideoCard { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(DisplaySize))]
        public int DisplaySizeId { get; set; }
        public virtual DisplaySize DisplaySize { get; set; } = null!;

        [ForeignKey(nameof(DisplayCoverage))]
        public int? DisplayCoverageId { get; set; }
        public virtual DisplayCoverage? DisplayCoverage { get; set; }

        [ForeignKey(nameof(DisplayTechnology))]
        public int? DisplayTechnologyId { get; set; }
        public virtual DisplayTechnology? DisplayTechnology { get; set; }

        [ForeignKey(nameof(Resolution))]
        public int? ResolutionId { get; set; }
        public virtual Resolution? Resolution { get; set; }

        [ForeignKey(nameof(Color))]
        public int? ColorId { get; set; }
        public virtual Color? Color { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Required]
        public int Warranty { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        [ForeignKey(nameof(Seller))]
        public int? SellerId { get; set; }
        public virtual Client? Seller { get; set; }
    }
}
