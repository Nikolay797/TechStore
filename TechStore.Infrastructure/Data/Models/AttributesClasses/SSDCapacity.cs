using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class SSDCapacity
    {
        public SSDCapacity()
        {
            SSDCapacityLaptops = new HashSet<Laptop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }

        public virtual ICollection<Laptop> SSDCapacityLaptops { get; set; }
    }
}