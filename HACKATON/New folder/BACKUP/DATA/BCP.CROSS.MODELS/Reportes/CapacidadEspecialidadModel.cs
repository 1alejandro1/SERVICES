using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes
{
    //SP=> SARC.SP_REP_CAPACIDAD_ESPECIALIDAD
    //SP Request=> FECHAINI, FECHAFIN
    public class CapacidadEspecialidadModel
    {
        public string Descripcion { get; set; } // SP=> DESCRIPCION
        public int Capacidad { get; set; }   // SP=> CAPACIDAD
    }
}
