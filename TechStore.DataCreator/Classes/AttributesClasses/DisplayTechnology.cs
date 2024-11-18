using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class DisplayTechnology
    {
        public DisplayTechnology()
        {
            this.DisplayTechnologies = new List<string>();
        }

        public IList<string> DisplayTechnologies { get; set; }
    }
}
