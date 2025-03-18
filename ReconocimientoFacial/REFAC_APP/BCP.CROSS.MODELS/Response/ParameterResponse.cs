using System.Collections.Generic;

namespace BCP.CROSS.MODELS.Response
{
    public class ParameterResponse
    {
        public string SessionID { get; set; }
        public List<ParameterDataResponse>? Data { get; set; }
    }
    public class ParameterDataResponse
    {
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }

}
