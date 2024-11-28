using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Television
{
    public class TelevisionEditViewModel : TelevisionImportViewModel
    {
        public int Id { get; init; }
        public Client? Seller { get; init; }
    }
}
