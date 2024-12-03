using TechStore.Core.Models.Product;

namespace TechStore.Core.Models.Smartwatch
{
	public class AllSmartwatchesQueryModel : AllProductsQueryModel
	{
		public AllSmartwatchesQueryModel()
		{
			this.Smartwatches = Enumerable.Empty<SmartwatchExportViewModel>();
		}

		public IEnumerable<SmartwatchExportViewModel> Smartwatches { get; set; }
	}
}
