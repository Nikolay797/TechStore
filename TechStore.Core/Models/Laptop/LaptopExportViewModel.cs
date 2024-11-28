using TechStore.Core.Contracts;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Laptop
{
    public class LaptopExportViewModel : ProductExportViewModel, IProductModel
    {
        public int Id { get; init; }
        public string Brand { get; init; } = null!;
        public string CPU { get; init; } = null!;
        public int RAM { get; init; }
        public int SSDCapacity { get; init; }
        public string VideoCard { get; init; } = null!;
        public decimal Price { get; init; }
        public double DisplaySize { get; init; }
        public int Warranty { get; init; }
    }
}
