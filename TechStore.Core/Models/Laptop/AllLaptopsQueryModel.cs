using TechStore.Core.Enums;

namespace TechStore.Core.Models.Laptop
{
    public class AllLaptopsQueryModel
    {
        public AllLaptopsQueryModel()
        {
            this.Cpus = Enumerable.Empty<string>();
            this.Rams = Enumerable.Empty<int>();
            this.SsdCapacities = Enumerable.Empty<int>();
            this.VideoCards = Enumerable.Empty<string>();
            this.CurrentPage = 1;
            this.Laptops = Enumerable.Empty<LaptopExportViewModel>();
        }

        public string? Cpu { get; init; }
        public IEnumerable<string> Cpus { get; set; }
        public int? Ram { get; init; }
        public IEnumerable<int> Rams { get; set; }
        public int? SsdCapacity { get; init; }
        public IEnumerable<int> SsdCapacities { get; set; }
        public string? VideoCard { get; init; }
        public IEnumerable<string> VideoCards { get; set; }
        public string? Keyword { get; init; }
        public Sorting Sorting { get; init; }
        public int CurrentPage { get; init; }
        public int TotalLaptopsCount { get; set; }
        public IEnumerable<LaptopExportViewModel> Laptops { get; set; }
    }
}
