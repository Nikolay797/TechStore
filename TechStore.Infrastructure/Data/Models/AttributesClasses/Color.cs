using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.ColorConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class Color
    {
        public Color()
        {
            ColorLaptops = new HashSet<Laptop>();
            ColorTelevisions = new HashSet<Television>();
            ColorKeyboards = new HashSet<Keyboard>();
            ColorMice = new HashSet<Mouse>();
            ColorHeadphones = new HashSet<Headphone>();
            ColorSmartWatches = new HashSet<SmartWatch>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(ColorNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> ColorLaptops { get; set; }
        public virtual ICollection<Television> ColorTelevisions { get; set; }
        public virtual ICollection<Keyboard> ColorKeyboards { get; set; }
        public virtual ICollection<Mouse> ColorMice { get; set; }
        public virtual ICollection<Headphone> ColorHeadphones { get; set; }
        public virtual ICollection<SmartWatch> ColorSmartWatches { get; set; }
    }
}