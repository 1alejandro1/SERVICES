using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ConsultaArea
{
    public class ConsultaArea
    {
        public string NroCarta { get; set; }
        public string UsuarioConsulta { get; set; }
        public string AreaConsulta { get; set; }
        public string Responsable { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Prioridad { get; set; }
        public string Respuesta { get; set; }
        public int Nivel { get; set; }
    }
}
