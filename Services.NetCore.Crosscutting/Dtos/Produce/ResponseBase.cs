namespace Services.NetCore.Crosscutting.Dtos.Produce
{
    public class ResponseBase
    {
        public string Message { get; set; }
        public string ValidationErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}