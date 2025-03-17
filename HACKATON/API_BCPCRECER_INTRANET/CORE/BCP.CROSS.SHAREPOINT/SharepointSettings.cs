using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SHAREPOINT
{
    public class SharepointSettings
    {
        public bool Simulacion { get; set; }
        public string PathSimulacion { get; set; }
        public string UriSharepoint { get; set; }
        public string MethodCopy { get; set; }
        public string MethodDws { get; set; }
        public string RutaCarpeta { get; set; }
        public string Dominio { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int TimeOut { get; set; }
        public string FolderRegistro { get; set; }
        public string FolderSolucion { get; set; }
        public string FolderRespuesta { get; set; }
        public string FolderLogMovimiento { get; set; }
    }
}
