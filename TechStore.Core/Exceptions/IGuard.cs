namespace TechStore.Core.Exceptions
{
    public interface IGuard
    {
        void AgainstNull<T>(T value, string? errorMessage = null);
        void AgainstNullOrEmptyCollection<T>(IEnumerable<T> collection, string? errorMessage = null);
    }
}
