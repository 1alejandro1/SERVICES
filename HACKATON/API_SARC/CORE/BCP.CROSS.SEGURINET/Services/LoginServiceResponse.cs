using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SEGURINET.Services
{
    public class LoginServiceResponse
    {
        public long CodigoSegurinet { get; set; }
        public string MensajeValidacion { get; set; }
        public bool ResultadoLogin { get; set; }
        public LoginServiceResponseData InformacionLogin { get; set; }
    }
}
