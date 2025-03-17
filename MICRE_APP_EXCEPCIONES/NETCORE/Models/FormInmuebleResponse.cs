namespace NETCORE.Models
{
    public class FormInmuebleResponse
    {
        public string? message { get; set; }
        public bool status { get; set; }
        public int code { get; set; }
        public FormInmuebleData? data { get; set; }
    }
}
