namespace TechStore.Core.Models.Keyboard
{
    public class KeyboardsQueryModel
    {
        public KeyboardsQueryModel()
        {
            this.Keyboards = new List<KeyboardExportViewModel>();
        }
        public int TotalKeyboardsCount { get; set; }
        public IEnumerable<KeyboardExportViewModel> Keyboards { get; set; }
    }
}
