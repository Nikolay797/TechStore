using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class CPU
    {
        public CPU()
        {
            this.LaptopCPUs = new List<string>();
        }

        public IList<string> LaptopCPUs { get; set; }
    }
}
