using TechStore.Core.Enums;

namespace TechStore.Core.Models.Mice
{
	public class AllMiceQueryModel
	{
		public AllMiceQueryModel()
		{
			this.Sensitivities = Enumerable.Empty<string>();
			this.Types = Enumerable.Empty<string>();

			this.CurrentPage = 1;

			this.Mice = Enumerable.Empty<MouseExportViewModel>();
		}

		public string? Sensitivity { get; init; }
		public IEnumerable<string> Sensitivities { get; set; }
		public string? Type { get; init; }
		public IEnumerable<string> Types { get; set; }
		public Wireless Wireless { get; init; }
		public string? Keyword { get; init; }
		public Sorting Sorting { get; init; }
		public int CurrentPage { get; init; }
		public int TotalMiceCount { get; set; }
		public IEnumerable<MouseExportViewModel> Mice { get; set; }
	}
}
