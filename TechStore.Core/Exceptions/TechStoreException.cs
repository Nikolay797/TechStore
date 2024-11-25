namespace TechStore.Core.Exceptions
{
    public class TechStoreException : ApplicationException
    {
        public TechStoreException()
        {

        }

        public TechStoreException(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}
