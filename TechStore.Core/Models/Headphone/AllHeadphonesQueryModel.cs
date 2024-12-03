using TechStore.Core.Enums;
using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Headphone
{
	public class AllHeadphonesQueryModel : AllProductsQueryModel
	{
		public AllHeadphonesQueryModel()
		{
			this.Types = Enumerable.Empty<string>();

			this.CurrentPage = 1;

			this.Headphones = Enumerable.Empty<HeadphoneExportViewModel>();
		}

		public string? Type { get; init; }
		public IEnumerable<string> Types { get; set; }
		public Wireless Wireless { get; init; }
		public IEnumerable<HeadphoneExportViewModel> Headphones { get; set; }
	}
}
