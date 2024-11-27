namespace TechStore.Core.Models.User
{
    public class UserExportViewModel
    {
        public UserExportViewModel()
        {
            this.Roles = Enumerable.Empty<string>();
        }
        public string Id { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public IEnumerable<string> Roles { get; init; }
    }
}
