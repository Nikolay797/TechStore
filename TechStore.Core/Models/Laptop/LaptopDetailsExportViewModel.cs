using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Laptop
{
    public class LaptopDetailsExportViewModel : LaptopExportViewModel
    {
        public string Type { get; init; } = null!;
        public string? DisplayCoverage { get; init; }
        public string? DisplayTechnology { get; init; }
        public string? Resolution { get; init; }
        public string? Color { get; init; }
        public string? ImageUrl { get; init; }
        public string AddedOn { get; init; } = null!;
        public int Quantity { get; init; }
        public Client? Seller { get; init; }
    }
}
