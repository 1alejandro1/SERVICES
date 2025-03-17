using System;
using System.Text.Json.Serialization;

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

    public class Meta
    {
        public string Msj { get; set; }
        public int StatusCode { get; set; }
        public string ResponseId { get; set; }
    }
}
