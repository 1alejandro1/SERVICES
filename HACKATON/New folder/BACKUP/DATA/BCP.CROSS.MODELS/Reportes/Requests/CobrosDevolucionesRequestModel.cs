using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes.Requests
{
    public class CobrosDevolucionesRequestModel
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Funcionario { get; set; }
        public int Canal { get; set; }
        public int Estado { get; set; }
        public int Tipo { get; set; }
    }
}
