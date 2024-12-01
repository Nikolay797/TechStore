using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Mice
{
	public class MouseEditViewModel : MouseImportViewModel, IProductModel
	{
		public int Id { get; init; }
		public Client? Seller { get; init; }
	}
}
