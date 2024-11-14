using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Infrastructure.Data.Models
{
    public class Client
    {
        public Client()
        {
            Laptops = new HashSet<Laptop>();
            Televisions = new HashSet<Television>();
            Keyboards = new HashSet<Keyboard>();
            Mice = new HashSet<Mouse>();
            Headphones = new HashSet<Headphone>();
            SmartWatches = new HashSet<SmartWatch>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int CountOfPurchases { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
        public virtual ICollection<Television> Televisions { get; set; }
        public virtual ICollection<Keyboard> Keyboards { get; set; }
        public virtual ICollection<Mouse> Mice { get; set; }
        public virtual ICollection<Headphone> Headphones { get; set; }
        public virtual ICollection<SmartWatch> SmartWatches { get; set; }
    }
}