using BCP.CROSS.MODELS.DTOs.Caso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ISharepointRepository
    {
        Task<bool> GuardarArchivosAdjuntosAsync(string nombreCarpeta, List<ArchivoSharepoint> archivosAdjuntos, string nombreProceso, bool base64);
    }
}
