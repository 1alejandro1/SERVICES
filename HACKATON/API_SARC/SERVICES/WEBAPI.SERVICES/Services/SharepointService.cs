using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class SharepointService : ISharepointService
    {
        private readonly ISharepointRepository _sharepointRepository;
        public SharepointService(ISharepointRepository sharepointRepository)
        {
            _sharepointRepository = sharepointRepository;
        }
        public async Task<ServiceResponse<bool>> GuardarArchivosAdjuntosAsync(ArchivosAdjuntos request, string requestId)
        {
            var responseServicio = await _sharepointRepository.GuardarArchivosAdjuntosAsync(request.nombreCarpeta,request.archivosAdjuntos,request.nombreProceso,request.base64);
            var response = new ServiceResponse<bool>
            {
                Data = responseServicio,
                Meta = {
                    Msj = responseServicio ? "Success" : "No se realizo la insercion de los archivos adjuntos",
                    ResponseId = requestId,
                    StatusCode = responseServicio ? 200 : 404
                }
            };

            return response;
        }
    }
}
