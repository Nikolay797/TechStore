using TechStore.Core.Contracts;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Headphone
{
	public class HeadphoneExportViewModel : ProductExportViewModel, IProductModel
	{
		public string Type { get; init; } = null!;
		public bool IsWireless { get; init; }
		public bool HasMicrophone { get; init; }
	}
}
