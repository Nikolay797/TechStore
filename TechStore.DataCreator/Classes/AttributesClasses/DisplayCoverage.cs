using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class DisplayCoverage
    {
        public DisplayCoverage()
        {
            this.DisplayCoverages = new List<string>();
        }

        public IList<string> DisplayCoverages { get; set; }
    }
}
