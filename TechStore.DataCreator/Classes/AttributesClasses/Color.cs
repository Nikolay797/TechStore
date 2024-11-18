using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class Color
    {
        public Color()
        {
            this.LaptopColors = new List<string>();
            this.TelevisionColors = new List<string>();
            this.KeyboardColors = new List<string>();
            this.MouseColors = new List<string>();
            this.HeadphoneColors = new List<string>();
            this.SmartWatchColors = new List<string>();
        }

        public IList<string> LaptopColors { get; set; }
        public IList<string> TelevisionColors { get; set; }
        public IList<string> KeyboardColors { get; set; }
        public IList<string> MouseColors { get; set; }
        public IList<string> HeadphoneColors { get; set; }
        public IList<string> SmartWatchColors { get; set; }
    }
}
