using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class CerrarCasoRequest
    {
        public string FuncionarioSupervisor { get; set; }
        public string NroCarta { get; set; }
        public string Producto { get; set; }
        public string Servicio { get; set; }
        public string Documento { get; set; }
        public string ErrorReg { get; set; }
        public string IdErrorReg { get; set; }
        public string DescripcionError { get; set; }
        public int CartasEnviadas { get; set; }
    }
}
