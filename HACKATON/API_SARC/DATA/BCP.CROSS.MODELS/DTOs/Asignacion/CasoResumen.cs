using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Asignacion
{
    public class CasoResumen
    {
        public string NroCaso { get; set; }
        public string Producto { get; set; }
        public string Servicio { get; set; }
        public string Detalle { get; set; }
        public string Complejidad { get; set; }
        public int Dias { get; set; }
        public string DescripcionError { get; set; }
        public string FuncionarioAtencion { get; set; }
    }
}
