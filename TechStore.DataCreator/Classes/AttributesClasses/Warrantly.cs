using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Warrantly
    {
        public Warrantly()
        {
            this.Warranties = new List<int>();
        }

        public IList<int> Warranties { get; set; }
    }
}
