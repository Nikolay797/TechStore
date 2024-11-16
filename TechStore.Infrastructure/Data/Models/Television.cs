using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Infrastructure.Data.Models
{
    public class Television
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(DisplaySize))]
        public int DisplaySizeId { get; set; }
        public virtual DisplaySize DisplaySize { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; } = null!;

        [ForeignKey(nameof(DisplayTechnology))]
        public int? DisplayTechnologyId { get; set; }
        public virtual DisplayTechnology? DisplayTechnology { get; set; }

        [Required]
        [ForeignKey(nameof(Resolution))]
        public int ResolutionId { get; set; }
        public virtual Resolution Resolution { get; set; } = null!;

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
