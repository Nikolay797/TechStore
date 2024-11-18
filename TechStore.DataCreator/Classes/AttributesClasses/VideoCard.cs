using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.DataCreator.Classes.AttributesClasses
{
    public class VideoCard
    {
        public VideoCard()
        {
            this.LaptopVideoCards = new List<string>();
        }

        public IList<string> LaptopVideoCards { get; set; }
    }
}
