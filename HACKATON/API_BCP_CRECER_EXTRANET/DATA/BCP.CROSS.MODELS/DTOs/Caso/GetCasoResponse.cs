using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class GetCasoResponse
    {
        public string NroCaso { get; set; }
        public string FuncionarioAtencion { get; set; }
        public string IdcCliente { get; set; }
        public string FuncionarioNombre { get; set; }
        public int Dias { get; set; }
        public string InformacionAdicional { get; set; }
        public string Estado { get; set; }
    }
}
