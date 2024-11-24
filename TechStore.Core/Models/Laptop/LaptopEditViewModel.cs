using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Laptop
{
    public class LaptopEditViewModel : LaptopImportViewModel
    {
        public int Id { get; init; }
        public Client? Seller { get; init; }
    }
}
