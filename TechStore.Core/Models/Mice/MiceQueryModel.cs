namespace TechStore.Core.Models.Mice
{
	public class MiceQueryModel
	{
		public MiceQueryModel()
		{
			this.Mice = new List<MouseExportViewModel>();
		}

		public int TotalMiceCount { get; set; }
		public IEnumerable<MouseExportViewModel> Mice { get; set; }
	}
}
