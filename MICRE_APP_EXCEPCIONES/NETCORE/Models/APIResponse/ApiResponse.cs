namespace NETCORE.Models.APIResponse
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public List<Excepcion>? Data { get; set; }
    }
}
