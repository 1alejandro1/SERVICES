using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes.CNR
{
    public class DetalleModel
    {

        public string Carta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Complejidad { get; set; }
        public int TSARC { get; set; }
        public int Tarea { get; set; }
        public string Tipo { get; set; }
    }
}
