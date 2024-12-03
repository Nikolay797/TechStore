namespace TechStore.Core.Models.Smartwatch
{
	public class SmartwatchesQueryModel
	{
		public SmartwatchesQueryModel()
		{
			this.Smartwatches = new List<SmartwatchExportViewModel>();
		}

		public int TotalSmartwatchesCount { get; set; }
		public IEnumerable<SmartwatchExportViewModel> Smartwatches { get; set; }
	}
}
