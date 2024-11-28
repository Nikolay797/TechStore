using TechStore.Core.Contracts;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Television
{
    public class TelevisionExportViewModel : ProductExportViewModel, IProductModel
    {
        public double DisplaySize { get; init; }
        public string DisplayTechnology { get; init; } = null!;
        public string Resolution { get; init; } = null!;
        public string DisplayCoverage { get; init; } = null!;
    }
}
