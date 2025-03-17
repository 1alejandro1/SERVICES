using System.Collections.Generic;

namespace OCR.Application.Api.Models
{
    public class ValidarCarnetIdentidadModel
    {
        public string operacion { get; set; }
        public List<string> texto { get; set; }
        public string image { get; set; }
        public bool valido { get; set; }
        public string idc { get; set; }
        public string fechaVencimiento { get; set; }
    }
   
}
