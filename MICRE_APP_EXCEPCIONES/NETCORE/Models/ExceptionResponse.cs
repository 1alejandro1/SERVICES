namespace NETCORE.Models
{
    public class ExceptionResponse
    {
        public string message { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public int code { get; set; }
        public List<int> data { get; set; } = new();
    }
}
