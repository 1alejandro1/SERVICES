namespace BCP.Framework.Common
{
    public class ApplicationSettings
    {
        public bool IncludeExceptionStackInResponse { get; set; }
        public string AllowedHosts { get; set; }    
        public string Channel { get; set; }
        public string AppUserId { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string PublicToken { get; set; }
    }
}
