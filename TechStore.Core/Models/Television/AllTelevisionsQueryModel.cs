using TechStore.Core.Enums;

namespace TechStore.Core.Models.Television
{
    public class AllTelevisionsQueryModel
    {
        public AllTelevisionsQueryModel()
        {
            this.Brands = Enumerable.Empty<string>();
            this.DisplaySizes = Enumerable.Empty<double>();
            this.Resolutions = Enumerable.Empty<string>();
            
            this.CurrentPage = 1;
            this.Televisions = Enumerable.Empty<TelevisionExportViewModel>();
        }

        public string? Brand { get; init; }
        public IEnumerable<string> Brands { get; set; }
        public double? DisplaySize { get; init; }
        public IEnumerable<double> DisplaySizes { get; set; }
        public string? Resolution { get; init; }
        public IEnumerable<string> Resolutions { get; set; }
        public string? Keyword { get; init; }
        public Sorting Sorting { get; init; }
        public int CurrentPage { get; init; }
        public int TotalTelevisionsCount { get; set; }
        public IEnumerable<TelevisionExportViewModel> Televisions { get; set; }
    }
}
