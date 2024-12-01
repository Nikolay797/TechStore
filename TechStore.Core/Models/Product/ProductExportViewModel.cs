namespace TechStore.Core.Models.Product
{
    public abstract class ProductExportViewModel
    {
        public int Id { get; init; }
        public string Brand { get; init; } = null!;
        public decimal? Price { get; init; }
        public int Warranty { get; init; }
    }
}
