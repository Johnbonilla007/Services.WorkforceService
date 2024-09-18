namespace Services.NetCore.Domain.Core
{
    public class DomainExceptionError : Exception
    {
        public DomainExceptionError()
        {

        }
        public DomainExceptionError(string message)
        {
            Message = message;
        }
        public DomainExceptionError(Exception exception)
        {
            Message = exception.Message;
        }

        public string Message { get; set; }
    }
}
