namespace BCP.CROSS.SECURITY.BasicAuthentication
{
    public class AuthorizationResponse
    {
        public string State { get; set; }
        public string Message { get; set; }
        public AuthorizationData Data { get; set; }
    }

    public class AuthorizationData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PublicToken { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
