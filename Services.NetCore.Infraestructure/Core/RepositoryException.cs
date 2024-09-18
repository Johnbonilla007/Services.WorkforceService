namespace Services.NetCore.Infraestructure.Core
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {

        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {

        }

        // You can add any additional properties or methods here
        public string ErrorCode { get; set; }
    }
}
