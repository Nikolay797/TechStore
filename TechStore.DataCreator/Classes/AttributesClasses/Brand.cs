using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Brand
    {
        public Brand()
        {
            this.LaptopBrands = new List<string>();
            this.TelevisionBrands = new List<string>();
            this.KeyboardBrands = new List<string>();
            this.MouseBrands = new List<string>();
            this.HeadphoneBrands = new List<string>();
            this.SmartWatchBrands = new List<string>();
        }

        public IList<string> LaptopBrands { get; set; }
        public IList<string> TelevisionBrands { get; set; }
        public IList<string> KeyboardBrands { get; set; }
        public IList<string> MouseBrands { get; set; }
        public IList<string> HeadphoneBrands { get; set; }
        public IList<string> SmartWatchBrands { get; set; }
    }
}
