namespace BCP.CROSS.MODELS
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public Meta Meta { get; set; }
        public ServiceResponse()
        {
            Meta = new Meta();
        }
    }
    public class ServiceResponseV2<T>
    {
        public T data { get; set; }
        public string state { get; set; }
        public string message { get; set; }
    }

    public class Meta
    {
        public string Msj { get; set; }
        public int StatusCode { get; set; }
        public string ResponseId { get; set; }
    }

   
}
