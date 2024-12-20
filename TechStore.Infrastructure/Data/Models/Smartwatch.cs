﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Color = TechStore.Infrastructure.Data.Models.AttributesClasses.Color;


namespace TechStore.Infrastructure.Data.Models
{
    public class SmartWatch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? ColorId { get; set; }

        [ForeignKey(nameof(ColorId))]
        public virtual Color? Color { get; set; }

        [MaxLength(2048)]
        public string? ImageUrl { get; set; }

        [Required]
        public int Warranty { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        public int? SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public Client? Seller { get; set; }
    }
}
