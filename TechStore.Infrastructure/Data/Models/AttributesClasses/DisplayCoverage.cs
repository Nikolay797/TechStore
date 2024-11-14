using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TechStore.Infrastructure.Constants.DataConstant.DisplayCoverageConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class DisplayCoverage
    {
        public DisplayCoverage()
        {
            DisplayCoverageLaptops = new HashSet<Laptop>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(DisplayCoverageNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> DisplayCoverageLaptops { get; set; }
    }
}
