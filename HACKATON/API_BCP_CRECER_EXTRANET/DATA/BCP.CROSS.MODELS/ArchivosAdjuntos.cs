using BCP.CROSS.MODELS.DTOs.Caso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS
{
    public class ArchivosAdjuntos
    {
        public string nombreCarpeta { get; set; }
        public List<ArchivoSharepoint> archivosAdjuntos { get; set; }
        public string nombreProceso { get; set; }
        public bool base64 { get; set; }
    }
}
