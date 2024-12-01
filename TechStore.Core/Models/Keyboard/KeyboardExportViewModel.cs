using TechStore.Core.Contracts;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Keyboard
{
    public class KeyboardExportViewModel : ProductExportViewModel, IProductModel
	{
        public bool IsWireless { get; init; }
        public string Type { get; init; } = null!;
        public string? Format { get; init; }
    }
}
