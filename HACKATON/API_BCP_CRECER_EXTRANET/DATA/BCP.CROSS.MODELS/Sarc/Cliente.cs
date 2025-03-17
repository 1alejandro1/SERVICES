using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Sarc
{
    public class Cliente
    {
        public string ClienteIdc { get; set; }
        public string ClienteTipo { get; set; }
        public string ClienteExtension { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string DireccionEnvio { get; set; }
        public string Fax { get; set; }
        public string TelefonoPrincipal { get; set; }
        public string TelefonoRespaldo { get; set; }
        public string CelularPrincipal { get; set; }
        public string CelularRespaldo { get; set; }
        public string Email { get; set; }

    }
}
