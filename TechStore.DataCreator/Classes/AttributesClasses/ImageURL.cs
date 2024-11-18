using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class ImageUrl
    {
        public ImageUrl()
        {
            this.LaptopImageUrls = new List<string>();
            this.TelevisionImageUrls = new List<string>();
            this.KeyboardImageUrls = new List<string>();
            this.MouseImageUrls = new List<string>();
            this.HeadphoneImageUrls = new List<string>();
            this.SmartWatchImageUrls = new List<string>();
        }

        public IList<string> LaptopImageUrls { get; set; }
        public IList<string> TelevisionImageUrls { get; set; }
        public IList<string> KeyboardImageUrls { get; set; }
        public IList<string> MouseImageUrls { get; set; }
        public IList<string> HeadphoneImageUrls { get; set; }
        public IList<string> SmartWatchImageUrls { get; set; }
    }
}
