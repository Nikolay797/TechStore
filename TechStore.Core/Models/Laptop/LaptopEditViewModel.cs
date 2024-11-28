using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Laptop
{
    public class LaptopEditViewModel : LaptopImportViewModel , IProductModel
    {
        public int Id { get; init; }
        public Client? Seller { get; init; }
    }
}
