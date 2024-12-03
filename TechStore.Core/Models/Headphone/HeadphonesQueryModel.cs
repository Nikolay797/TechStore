namespace TechStore.Core.Models.Headphone
{
	public class HeadphonesQueryModel
	{
		public HeadphonesQueryModel()
		{
			this.Headphones = new List<HeadphoneExportViewModel>();
		}

		public int TotalHeadphonesCount { get; set; }
		public IEnumerable<HeadphoneExportViewModel> Headphones { get; set; }
	}
}
