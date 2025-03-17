using System.Collections.Generic;

namespace OCR.Application.Api.Models
{
    public class ValidateDocumentResponse
    {
        public string operacion { get; set; }
        public List<string> texto { get; set; }
    }
}
