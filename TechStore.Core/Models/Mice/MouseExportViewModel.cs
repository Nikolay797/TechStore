using TechStore.Core.Contracts;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Mice
{
	public class MouseExportViewModel : ProductExportViewModel, IProductModel
	{
		public bool IsWireless { get; init; }
		public string Type { get; init; } = null!;
		public string Sensitivity { get; init; } = null!;
	}
}
