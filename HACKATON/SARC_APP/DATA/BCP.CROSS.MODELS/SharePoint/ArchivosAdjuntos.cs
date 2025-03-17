using System;
using System.Collections.Generic;
using System.Text;
using BCP.CROSS.MODELS.Caso;

namespace BCP.CROSS.MODELS.SharePoint
{
    public class ArchivosAdjuntos
    {
        public string nombreCarpeta { get; set; }
        public List<ArchivoAdjunto> archivosAdjuntos { get; set; }
        public string nombreProceso { get; set; }
        public bool base64 { get; set; }
    }
}
