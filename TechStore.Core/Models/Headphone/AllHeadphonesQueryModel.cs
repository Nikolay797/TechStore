using TechStore.Core.Enums;

namespace TechStore.Core.Models.Headphone
{
	public class AllHeadphonesQueryModel
	{
		public AllHeadphonesQueryModel()
		{
			this.Types = Enumerable.Empty<string>();

			this.CurrentPage = 1;

			this.Headphones = Enumerable.Empty<HeadphoneExportViewModel>();
		}

		public string? Type { get; init; }
		public IEnumerable<string> Types { get; set; }
		public Wireless Wireless { get; init; }
		public string? Keyword { get; init; }
		public Sorting Sorting { get; init; }
		public int CurrentPage { get; init; }
		public int TotalHeadphonesCount { get; set; }
		public IEnumerable<HeadphoneExportViewModel> Headphones { get; set; }
	}
}
