using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataCreator.Classes.BaseClass;

namespace TechStore.DataCreator.Classes
{
    public class Keyboard : Product
    {
        public string Format { get; set; } = null!;

        public string Type { get; set; } = null!;
    }
}
