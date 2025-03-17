using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ISharepointService
    {
        Task<ServiceResponse<bool>> GuardarArchivosAdjuntosAsync(ArchivosAdjuntos request, string requestId);
    }
}
