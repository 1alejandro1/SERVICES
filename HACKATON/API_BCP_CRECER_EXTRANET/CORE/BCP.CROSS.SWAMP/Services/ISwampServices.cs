using BCP.CROSS.MODELS.Swamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SWAMP.Services
{
    public interface ISwampServices
    {
        Task<GetDatosBasicosTicketServicesResponse> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request);
        Task<InsertEventSwampResponse> InsertEventoSwampAsync(InsertEventoRequest request);
        Task<TipoCambioSwampResponse> TipoCambioResponse(bool request = false);
    }
}
