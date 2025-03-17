using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Swamp
{
    public  class DatosBasicosTicketResponse
    {
        public string ClienteIdc { get; set; }
        public string ClienteExtension { get; set; }
        public string ClienteTipo { get; set; }
        public string ClienteCic { get; set; }
        public string Aplicativo { get; set; }
        public string Ticket { get; set; }
    }
}
