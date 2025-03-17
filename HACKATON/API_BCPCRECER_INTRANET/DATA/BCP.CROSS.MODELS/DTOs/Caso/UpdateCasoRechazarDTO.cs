using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateCasoRechazarDTO
    {
        public string NroCaso { get; set; }
        public string SW { get; set; }
        public string Descripcion { get; set; }
        public string TipoError { get; set; }
        public string FechaProrroga { get; set; }
    }
}
