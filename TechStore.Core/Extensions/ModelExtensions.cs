using TechStore.Core.Contracts;

namespace TechStore.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IProductModel productModel)
        {
            var brand = productModel.Brand.Replace("-", "");
            var price = $"{productModel.Price:F2}";
            var information = brand + "-" + price;
            return information;
        }
    }
}
