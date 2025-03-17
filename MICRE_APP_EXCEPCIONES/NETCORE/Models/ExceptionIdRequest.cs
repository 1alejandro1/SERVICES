using NETCORE.Models;

namespace MICRE_APP_EXCEPCIONES.Models
{
    public class ExceptionIdRequest
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public List<ExceptionIdData>? Data { get; set; }
    }
}
