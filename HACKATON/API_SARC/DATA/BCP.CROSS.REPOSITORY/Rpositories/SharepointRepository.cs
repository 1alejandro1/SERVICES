using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.SHAREPOINT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class SharepointRepository: ISharepointRepository
    {
        public readonly Sharepoint _sharepoint;
        public SharepointRepository(Sharepoint sharepoint)
        {
            _sharepoint = sharepoint;
        }

        public async Task<bool> GuardarArchivosAdjuntosAsync(string nombreCarpeta, List<ArchivoSharepoint> archivosAdjuntos, string nombreProceso, bool base64)
        {
            return await _sharepoint.GuardarArchivosAdjuntosAsync(nombreCarpeta, archivosAdjuntos, nombreProceso, base64);
        }
    }
}
