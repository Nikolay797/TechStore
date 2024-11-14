using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Color = System.Drawing.Color;

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
