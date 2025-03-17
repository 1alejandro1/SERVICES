using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Sarc
{
    //exec SARC.SP_CARTA_REMPLAZADA @TIPO_CARTA=N'01',@NRO_CARTA=N'BC1987-20211103-105148'
    public class CartaFileRequest
    {
        public string TipoCarta { get; set; }//=>TIPO_CARTA
        public string NroCarta { get; set; } //=> NRO_CARTA
    }
}
