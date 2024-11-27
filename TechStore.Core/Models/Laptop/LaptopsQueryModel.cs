namespace TechStore.Core.Models.Laptop
{
	public class LaptopsQueryModel
	{
		public LaptopsQueryModel()
		{
            this.Laptops = new List<LaptopExportViewModel>();
        }

        public int TotalLaptopsCount { get; set; }
        public IEnumerable<LaptopExportViewModel> Laptops { get; set; }
	}
}
