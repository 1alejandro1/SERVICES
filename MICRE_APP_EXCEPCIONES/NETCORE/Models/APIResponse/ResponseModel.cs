using System.Reflection.Metadata;

namespace NETCORE.Models.APIResponse
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public List<Parametro>? Data { get; set; }
    }
}
