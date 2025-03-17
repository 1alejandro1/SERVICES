namespace BCP.CROSS.LOGGER
{
    public class LoggerOptions
    {
        public string Path { get; set; }
        public string File { get; set; }
        public string Template { get; set; }
        public int FileSizeLimitMegaBytes { get; set; }
        public string LogEventLevel { get; set; }
    }
}