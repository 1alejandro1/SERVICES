namespace NETCORE.Models
{
    public class TokenResponse
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public TokenData? Data { get; set; }
    }
}
