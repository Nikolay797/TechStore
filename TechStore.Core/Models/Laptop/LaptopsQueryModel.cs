using TechStore.Core.Enums;

namespace TechStore.Core.Models.Laptop
{
	public class LaptopsQueryModel
	{
		public LaptopsQueryModel()
		{
			this.Cpus = Enumerable.Empty<string>();
			this.Rams = Enumerable.Empty<int>();
			this.SsdCapacities = Enumerable.Empty<int>();
			this.VideoCards = Enumerable.Empty<string>();
			this.Laptops = Enumerable.Empty<LaptopExportViewModel>();
		}

		public string? Cpu { get; set; }
		public IEnumerable<string> Cpus { get; set; }
		public int? Ram { get; set; }
		public IEnumerable<int> Rams { get; set; }
		public int? SsdCapacity { get; set; }
		public IEnumerable<int> SsdCapacities { get; set; }
		public string? VideoCard { get; set; }
		public IEnumerable<string> VideoCards { get; set; }
		public string? KeyWord { get; set; }
		public Sorting Sorting { get; set; }
		public IEnumerable<LaptopExportViewModel> Laptops { get; set; }
	}
}
