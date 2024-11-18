using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataCreator.Classes.BaseClass;

namespace TechStore.DataCreator.Classes
{
    public class Mouse : Product
    {
        public string Type { get; set; } = null!;

        public string Sensitivity { get; set; } = null!;
    }
}
