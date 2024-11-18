using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class SSDCapacity
    {
        public SSDCapacity()
        {
            this.LaptopSSDCapacities = new List<int>();
        }

        public IList<int> LaptopSSDCapacities { get; set; }
    }
}
