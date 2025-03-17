using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Swamp;
using BCP.CROSS.SWAMP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.CROSS.SWAMP
{
    public class Swamp : ISwamp
    {
        private readonly ISwampServices _swampServices;
        public Swamp(ISwampServices swampServices)
        {
            this._swampServices= swampServices;
        }

        public async Task<DatosBasicosTicketResponse> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request)
        {
            var responseService =await this._swampServices.GetDatosBasicosTicketAsync(request);
            if (responseService != null)
            {
                var response = new DatosBasicosTicketResponse
                {
                    ClienteIdc= responseService.idc,
                    ClienteTipo=responseService.tipo,
                    ClienteExtension=responseService.extension,
                    ClienteCic=responseService.cic,
                    Aplicativo=responseService.aplicativo,
                    Ticket=responseService.ticket,
                };
                return response;
            }
            return null;
        }

        public async Task<bool> InsertEventAsync(InsertEventoRequest request)
        {
            bool response = false;
            var responseService = await this._swampServices.InsertEventoSwampAsync(request);
            if (responseService != null)
            {
                response = responseService.state == 200;
            }
            return response;
        }
        public async Task<TipoCambio> GetTipoCambioSwampAsync()
        {
            var responseService = await this._swampServices.TipoCambioResponse();
            if (responseService != null)
            {
                var response = new TipoCambio
                {
                    Compra = responseService.compra,
                    Venta = responseService.venta
                };
                return response;
            }
            return null;
        }
    }
}
