using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TechStore.Infrastructure.Constants.DataConstant.SensitivityConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class Sensitivity
    {
        public Sensitivity()
        {
            SensitivityMice = new HashSet<Mouse>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(SensitivityRangeMaxLength)]
        public string Range { get; set; } = null!;

        public virtual ICollection<Mouse> SensitivityMice { get; set; }
    }
}