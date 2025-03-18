using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Response
{
    public class DocumentLoadResponse
    {
        public string SessionId { get; set; }
        public DataDocumentLoadResponse? Data { get; set; }
        public string? Message { get; set; }
    }
    public class DataDocumentLoadResponse
    {
        public bool State { get; set; }
        public string? Message { get; set; }
    }

}
