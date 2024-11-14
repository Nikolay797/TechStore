using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechStore.Infrastructure.Data.Models;

using static TechStore.Infrastructure.Constants.DataConstant.CPUConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class CPU
    {
        public CPU()
        {
            CPULaptops = new HashSet<Laptop>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(CPUNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> CPULaptops { get; set; }
    }
}
