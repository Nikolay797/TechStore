using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataCreator.Classes.BaseClass;

namespace TechStore.DataCreator.Classes
{
    public class Television : Product
    {
        public double DisplaySize { get; set; }
        public string Type { get; set; } = null!;
        public string DisplayTechnology { get; set; } = null!;
        public string Resolution { get; set; } = null!;
    }
}
