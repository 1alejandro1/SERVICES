using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Response
{
    public class DataSessionResponse
    {
        public string SessionId { get; set; }
        public DataSessionDataResponse Data { get; set; }
    }
    public class DataSessionDataResponse
    {
        public bool carnetAnverso { get; set; }
        public bool carnetReverso { get; set; }
        public bool pruebaVida { get; set; }
        public int idCanal { get; set; }
        public bool Pasiva { get; set; }
        public bool esMenor { get; set; }
    }
}
