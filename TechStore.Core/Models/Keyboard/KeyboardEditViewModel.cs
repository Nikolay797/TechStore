using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Keyboard
{
	public class KeyboardEditViewModel : KeyboardImportViewModel, IProductModel
	{
		public int Id { get; init; }
		public Client? Seller { get; init; }
	}
}
