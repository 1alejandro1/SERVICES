using System.Collections.Generic;

namespace OCR.Application.Api.Models
{
    public class ValidarDocumentoResponse
    {
        public string operacion { get; set; }
        public List<string> texto { get; set; }
        public string image { get; set; }
    }
}
