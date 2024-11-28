using TechStore.Core.Contracts;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Television
{
    public class TelevisionEditViewModel : TelevisionImportViewModel , IProductModel
    {
        public int Id { get; init; }
        public Client? Seller { get; init; }
    }
}
