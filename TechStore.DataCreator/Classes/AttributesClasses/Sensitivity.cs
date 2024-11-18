using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Sensitivity
    {
        public Sensitivity()
        {
            this.MouseSensitivities = new List<string>();
        }

        public IList<string> MouseSensitivities { get; set; }
    }
}
