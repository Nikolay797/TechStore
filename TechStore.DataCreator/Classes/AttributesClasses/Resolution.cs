using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Resolution
    {
        public Resolution()
        {
            this.LaptopResolutions = new List<string>();
            this.TelevisionResolutions = new List<string>();
        }

        public IList<string> LaptopResolutions { get; set; }

        public IList<string> TelevisionResolutions { get; set; }
    }
}
