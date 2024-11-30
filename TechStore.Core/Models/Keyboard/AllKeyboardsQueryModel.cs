using TechStore.Core.Enums;

namespace TechStore.Core.Models.Keyboard
{
    public class AllKeyboardsQueryModel
    {
        public AllKeyboardsQueryModel()
        {
            this.Formats = Enumerable.Empty<string>();
            this.Types = Enumerable.Empty<string>();
            this.CurrentPage = 1;
            this.Keyboards = Enumerable.Empty<KeyboardExportViewModel>();
        }

        public string? Format { get; init; }
        public IEnumerable<string> Formats { get; set; }
        public string? Type { get; init; }
        public IEnumerable<string> Types { get; set; }
        public Wireless Wireless { get; init; }
        public string? Keyword { get; init; }
        public Sorting Sorting { get; init; }
        public int CurrentPage { get; init; }
        public int TotalKeyboardsCount { get; set; }
        public IEnumerable<KeyboardExportViewModel> Keyboards { get; set; }
    }
}
