using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class RAM
    {
        public RAM()
        {
            RAMLaptops = new HashSet<Laptop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RAM value must be a positive integer.")]
        public int Value { get; set; }

        public virtual ICollection<Laptop> RAMLaptops { get; set; }
    }
}