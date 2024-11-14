using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechStore.Infrastructure.Data.Models;
using static TechStore.Infrastructure.Constants.DataConstant.TypeConstants;

namespace TechStore.Infrastructure.Data.Models.AttributesClasses
{
    public class Type
    {
        public Type()
        {
            TypeLaptops = new HashSet<Laptop>();
            TypeTelevisions = new HashSet<Television>();
            TypeKeyboards = new HashSet<Keyboard>();
            TypeMice = new HashSet<Mouse>();
            TypeHeadphones = new HashSet<Headphone>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> TypeLaptops { get; set; }
        public virtual ICollection<Television> TypeTelevisions { get; set; }
        public virtual ICollection<Keyboard> TypeKeyboards { get; set; }
        public virtual ICollection<Mouse> TypeMice { get; set; }
        public virtual ICollection<Headphone> TypeHeadphones { get; set; }
    }
}
