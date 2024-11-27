namespace TechStore.Core.Models.Television
{
    public class TelevisionsQueryModel
    {
        public TelevisionsQueryModel()
        {
            this.Televisions = new List<TelevisionExportViewModel>();
        }

        public int TotalTelevisionsCount { get; set; }
        public IEnumerable<TelevisionExportViewModel> Televisions { get; set; }
    }
}
