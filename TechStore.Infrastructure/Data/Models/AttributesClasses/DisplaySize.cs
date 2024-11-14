using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class DisplaySize
    {
        public DisplaySize()
        {
            DisplaySizeLaptops = new HashSet<Laptop>();
            DisplaySizeTelevisions = new HashSet<Television>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        public virtual ICollection<Laptop> DisplaySizeLaptops { get; set; }
        public virtual ICollection<Television> DisplaySizeTelevisions { get; set; }
    }
}