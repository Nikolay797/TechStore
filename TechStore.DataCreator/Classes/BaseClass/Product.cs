using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.BaseClass
{
    public class Product
    {
        public string Brand { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Warranty { get; set; }
    }
}
