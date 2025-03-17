using System;
using System.Collections.Generic;
using System.Text;
using BCP.CROSS.MODELS.Caso;
using Microsoft.AspNetCore.Http;

namespace BCP.CROSS.MODELS.SharePoint
{
    public class ArchivosAdjuntosDTO
    {
        public string nombreCarpeta { get; set; }
        public List<IFormFile> archivosAdjuntos { get; set; }
        public string nombreProceso { get; set; }
    }
}
