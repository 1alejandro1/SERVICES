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
    public class SmartLinkService : ISmartLinkService
    {
        private readonly ISmartLinkRepository _smartLink;
        public SmartLinkService(ISmartLinkRepository smartLink)
        {
            this._smartLink=smartLink;
        }
        public async Task<ServiceResponse<TipoCambio>> GetTipoCambioAsync(string requestId)
        {
            var responseServicio = await _smartLink.GetTipoCambioAsync();
            var response = new ServiceResponse<TipoCambio>
            {
                Data = responseServicio,
                Meta = {
                    Msj = responseServicio != null ? "Success" : "No se pudo obtener los datos de SmartLink",
                    ResponseId = requestId,
                    StatusCode = responseServicio != null ? 200 : 404
                }
            };

            return response;
        }
    }
}
