using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class RAM
    {
        public RAM()
        {
            this.LaptopRAMs = new List<int>();
        }

        public IList<int> LaptopRAMs { get; set; }
    }
}
