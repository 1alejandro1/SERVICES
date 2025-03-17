using BCP.CROSS.MODELS.DTOs.Caso;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCP.CROSS.SHAREPOINT
{
    public interface ISharepoint
    {
        HealthCheckResult Check();
        Task<bool> GuardarArchivosAdjuntosAsync(string nombreCarpeta, List<ArchivoSharepoint> archivosAdjuntos, string nombreProceso, bool base64);
        string GetRutaSharePoint();
    }
}
