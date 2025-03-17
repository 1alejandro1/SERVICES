using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateCasoSolucionDateDTO: UpdateCasoSolucionDTO
    {
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }
    }
}
