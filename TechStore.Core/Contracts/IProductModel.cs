namespace TechStore.Core.Contracts
{
    public interface IProductModel
    {
        public string Brand { get; }
        public decimal? Price { get; }
    }
}
