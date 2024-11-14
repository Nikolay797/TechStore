using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.ResolutionConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class Resolution
    {
        public Resolution()
        {
            ResolutionLaptops = new HashSet<Laptop>();
            ResolutionTelevisions = new HashSet<Television>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ResolutionValueMaxLength)]
        public string Value { get; set; } = null!;

        public virtual ICollection<Laptop> ResolutionLaptops { get; set; }

        public virtual ICollection<Television> ResolutionTelevisions { get; set; }
    }
}