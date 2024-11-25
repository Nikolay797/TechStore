namespace TechStore.Core.Exceptions
{
    public class Guard : IGuard
    {
		public void AgainstClientThatDoesNotExist<T>(T value, string? errorMessage = null)
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

        public void AgainstProductThatIsNull<T>(T value, string? errorMessage = null)
        {
	        if (value is null)
	        {
				var exception = errorMessage is null
					? new ArgumentException()
					: new ArgumentException(errorMessage);
				throw exception;
			}
        }

        public void AgainstProductThatIsDeleted(bool isDeleted, string? errorMessage = null)
        {
	        if (isDeleted)
	        {
				var exception = errorMessage is null
					? new ArgumentException()
					: new ArgumentException(errorMessage);
				throw exception;
			}
        }

        public void AgainstProductThatIsOutOfStock(int quantity, string? errorMessage = null)
        {
	        if (quantity == 0)
	        {
				var exception = errorMessage is null
					? new ArgumentException()
					: new ArgumentException(errorMessage);
				throw exception;
			}
        }
	}
}
