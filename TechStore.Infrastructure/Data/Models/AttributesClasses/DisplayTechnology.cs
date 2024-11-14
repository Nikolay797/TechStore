using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.DisplayTechnologyConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class DisplayTechnology
    {
        public DisplayTechnology()
        {
            DisplayTechnologyLaptops = new HashSet<Laptop>();
            DisplayTechnologyTelevisions = new HashSet<Television>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DisplayTechnologyNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> DisplayTechnologyLaptops { get; set; }
        public virtual ICollection<Television> DisplayTechnologyTelevisions { get; set; }
    }
}