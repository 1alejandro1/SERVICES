using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Swamp;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.SWAMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class SwampRepository: ISwampRepository
    {
        private readonly Swamp _swampCore;

        public SwampRepository(Swamp swampCore)
        {
            this._swampCore = swampCore;
        }

        public async Task<DatosBasicosTicketResponse> GetDatosBasicosTicketAsync(DatosBasicosTicketRequest request)
        {
            return await this._swampCore.GetDatosBasicosTicketAsync(request);
        }

        public async Task<TipoCambio> GetTipoCambioSwampAsync()
        {
            return await this._swampCore.GetTipoCambioSwampAsync();
        }

        public  async Task<bool> InsertEventAsync(InsertEventoRequest request)
        {
            return await this._swampCore.InsertEventAsync(request);
        }
    }
}
