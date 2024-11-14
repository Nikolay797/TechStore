using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TechStore.Infrastructure.Constants.DataConstant.FormatConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class Format
    {
        public Format()
        {
            this.FormatKeyboards = new HashSet<Keyboard>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FormatNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Keyboard> FormatKeyboards { get; set; }
    }
}
