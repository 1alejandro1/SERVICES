using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Swamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ISwampService
    {
        Task<ServiceResponse<DatosBasicosTicketResponse>> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request, string requestId);
        Task<ServiceResponse<bool>> InsertEventAsync(InsertEventoRequest request, string requestId);
        Task<ServiceResponse<TipoCambio>> GetTipoCambioSwampAsync(string requestId);
    }
}
