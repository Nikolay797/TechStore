namespace TechStore.Core.Exceptions
{
    public class Guard : IGuard
    {
        public void AgainstNull<T>(T value, string? errorMessage = null)
        {
            if (value is null)
            {
                var exception = errorMessage is null
                    ? new TechStoreException()
                    : new TechStoreException(errorMessage);
                throw exception;
            }
        }

        public void AgainstNullOrEmptyCollection<T>(IEnumerable<T> collection, string? errorMessage = null)
        {
            if (collection is null || !collection.Any())
            {
                var exception = errorMessage is null
                    ? new TechStoreException()
                    : new TechStoreException(errorMessage);
                throw exception;
            }
        }
    }
}
