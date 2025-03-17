using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateCasoDTOViaEnvio
    {

        public string NroCarta { get; set; }
        public string ViaEnvio { get; set; }
        public string RespuestaEnviada { get; set; }
        public string ComTelefono { get; set; }
        public string ComSMS { get; set; }
        public string ComOficina { get; set; }
        public string ComEmail { get; set; }
        public string ComWhastapp { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string SMS { get; set; }
    }
}
