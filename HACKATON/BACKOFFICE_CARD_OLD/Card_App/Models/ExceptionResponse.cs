namespace Card_App.Models
{
    public class ExceptionResponse
    {
        public string ErrorMessage { get; set; }

        public string CorrelationIdentifier { get; set; }

        public string InnerException { get; set; }
    }
}
