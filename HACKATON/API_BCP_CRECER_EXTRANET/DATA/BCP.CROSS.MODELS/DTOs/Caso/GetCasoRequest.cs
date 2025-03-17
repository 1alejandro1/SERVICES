using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class GetCasoRequest
    {
        public string Producto { get; set; }
        public string Servicio { get; set; }
        public string Estado { get; set; }
        public string TipoCaso { get; set; }
    }
}
