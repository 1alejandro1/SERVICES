using System;

namespace BCP.Framework.Common
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode{ get; set; }
    }
}
