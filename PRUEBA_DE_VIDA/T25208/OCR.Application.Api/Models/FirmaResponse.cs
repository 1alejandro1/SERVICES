using System.Collections.Generic;

namespace OCR.Application.Api.Models
{
    public class FirmaResponse
    {
        public string operacion { get; set; }
        public List<string> data { get; set; }
        public class Description
        {
            public string text { get; set; }
            public string confidence { get; set; }
        }
    }
}
