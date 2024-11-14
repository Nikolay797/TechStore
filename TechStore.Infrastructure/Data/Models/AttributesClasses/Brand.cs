using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.BrandConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class Brand
    {
        public Brand()
        {
            BrandLaptops = new HashSet<Laptop>();
            BrandTelevisions = new HashSet<Television>();
            BrandKeyboards = new HashSet<Keyboard>();
            BrandMice = new HashSet<Mouse>();
            BrandHeadphones = new HashSet<Headphone>();
            BrandSmartWatches = new HashSet<SmartWatch>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> BrandLaptops { get; set; }
        public virtual ICollection<Television> BrandTelevisions { get; set; }
        public virtual ICollection<Keyboard> BrandKeyboards { get; set; }
        public virtual ICollection<Mouse> BrandMice { get; set; }
        public virtual ICollection<Headphone> BrandHeadphones { get; set; }
        public virtual ICollection<SmartWatch> BrandSmartWatches { get; set; }
    }
}