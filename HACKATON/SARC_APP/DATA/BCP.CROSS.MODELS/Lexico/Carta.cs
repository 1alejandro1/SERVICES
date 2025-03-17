using System;
using System.Collections.Generic;
using System.Linq;

namespace BCP.CROSS.MODELS.Lexico
{
    public class Carta: CartaIdRequest
    {        
        public string Descripcion { get; set; }
        public string Cuerpo { get; set; }
    }

    public class CartaIdRequest
    {
        public string CartaId { get; set; }
    }

}