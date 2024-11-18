using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Type
    {
        public Type()
        {
            this.LaptopTypes = new List<string>();
            this.TelevisionTypes = new List<string>();
            this.KeyboardTypes = new List<string>();
            this.MouseTypes = new List<string>();
            this.HeadphoneTypes = new List<string>();
        }

        public IList<string> LaptopTypes { get; set; }
        public IList<string> TelevisionTypes { get; set; }
        public IList<string> KeyboardTypes { get; set; }
        public IList<string> MouseTypes { get; set; }
        public IList<string> HeadphoneTypes { get; set; }
    }
}
