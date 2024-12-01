namespace TechStore.Core.Exceptions
{
    public interface IGuard
    {
        void AgainstInvalidUserId<T>(T value, string? errorMessage = null);
        void AgainstNullOrEmptyCollection<T>(IEnumerable<T> collection, string? errorMessage = null);
		void AgainstProductThatIsDeleted(bool isDeleted, string? errorMessage = null);
		void AgainstProductThatIsOutOfStock(int quantity, string? errorMessage = null);
		void AgainstProductThatIsNull<T>(T value, string? errorMessage = null);
		void AgainstNotExistingValue<T>(T value, string? errorMessage = null);
	}
}
