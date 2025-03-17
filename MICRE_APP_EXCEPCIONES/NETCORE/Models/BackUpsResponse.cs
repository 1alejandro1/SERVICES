using NETCORE.Models.APIResponse;

namespace MICRE_APP_EXCEPCIONES.Models
{
    public class BackUpsResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public List<BackUpsData>? Data { get; set; }
    }
}
