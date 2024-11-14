﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Infrastructure.Data.Models
{
    public class Mouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsWireless { get; set; }

        [Required]
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Sensitivity))]
        public int SensitivityId { get; set; }
        public virtual Sensitivity Sensitivity { get; set; } = null!;

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