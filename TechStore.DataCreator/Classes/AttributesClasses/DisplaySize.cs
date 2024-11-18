using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class DisplaySize
    {
        public DisplaySize()
        {
            this.LaptopDisplaySizes = new List<double>();
            this.TelevisionDisplaySizes = new List<double>();
        }

        public IList<double> LaptopDisplaySizes { get; set; }

        public IList<double> TelevisionDisplaySizes { get; set; }
    }
}
