using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Format
    {
        public Format()
        {
            this.KeyboardFormats = new List<string>();
        }

        public IList<string> KeyboardFormats { get; set; }
    }
}
