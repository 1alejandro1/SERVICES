using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Carta
{
    public class Carta: CartaIdRequest
    {        
        public string Descripcion { get; set; }
        public string Cuerpo { get; set; }
    }
}
