using TechStore.Core.Enums;

namespace TechStore.Core.Models.Product
{
	public class AllProductsQueryModel
	{
		public string? Keyword { get; init; }
		public Sorting Sorting { get; init; }
		public int CurrentPage { get; init; } = 1;
		public int TotalProductsCount { get; set; }
	}
}
