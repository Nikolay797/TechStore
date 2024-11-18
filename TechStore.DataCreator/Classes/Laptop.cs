using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataCreator.Classes.BaseClass;

namespace TechStore.DataCreator.Classes
{
    public class Laptop : Product
    {
        public string CPU { get; set; } = null!;
        public int RAM { get; set; }
        public int SSDCapacity { get; set; }
        public string VideoCard { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double DisplaySize { get; set; }
        public string DisplayCoverage { get; set; } = null!;
        public string DisplayTechnology { get; set; } = null!;
        public string Resolution { get; set; } = null!;
    }
}
