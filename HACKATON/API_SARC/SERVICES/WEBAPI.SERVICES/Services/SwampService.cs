using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Swamp;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class SwampService : ISwampService
    {
        private readonly ISwampRepository _swampRepository;
        public SwampService(ISwampRepository swampRepository)
        {
            this._swampRepository= swampRepository;
        }
        public async Task<ServiceResponse<DatosBasicosTicketResponse>> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request, string requestId)
        {
            var responseServicio = await _swampRepository.GetDatosBasicosTicketAsync(request);
            var response = new ServiceResponse<DatosBasicosTicketResponse>
            {
                Data = responseServicio,
                Meta = {
                    Msj = responseServicio != null ? "Success" : "No se pudo recuperar la informacion asociada al guid del cliente",
                    ResponseId = requestId,
                    StatusCode = responseServicio != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<TipoCambio>> GetTipoCambioSwampAsync(string requestId)
        {
            var responseServicio = await _swampRepository.GetTipoCambioSwampAsync();
            var response = new ServiceResponse<TipoCambio>
            {
                Data = responseServicio,
                Meta = {
                    Msj = responseServicio != null ? "Success" : "No se pudo recuperar la informacion asociada al Tipo de Cambio",
                    ResponseId = requestId,
                    StatusCode = responseServicio != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> InsertEventAsync(InsertEventoRequest request, string requestId)
        {
            var responseServicio = await _swampRepository.InsertEventAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = responseServicio,
                Meta = {
                    Msj = responseServicio? "Success" : "No se pudo registrar la informacion de la sesion",
                    ResponseId = requestId,
                    StatusCode = responseServicio? 200 : 404
                }
            };

            return response;
        }
    }
}
