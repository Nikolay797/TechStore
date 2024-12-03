using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Smartwatch
{
	public class SmartwatchEditViewModel : SmartwatchImportViewModel, IProductModel
	{
		public int Id { get; init; }
		public Client? Seller { get; init; }
	}
}
