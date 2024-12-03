using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Headphone
{
	public class HeadphoneEditViewModel : HeadphoneImportViewModel, IProductModel
	{
		public int Id { get; init; }
		public Client? Seller { get; init; }
	}
}
