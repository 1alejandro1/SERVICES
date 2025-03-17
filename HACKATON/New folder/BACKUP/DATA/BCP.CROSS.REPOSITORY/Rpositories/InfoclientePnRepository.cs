using BCP.CROSS.INFOCLIENTE;
using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Cliente.Intermedio;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class InfoclientePnRepository: IInfoclientePnRepository
    {
        private readonly InfoclientePnl _infocliente;

        public InfoclientePnRepository(InfoclientePnl infocliente)
        {
            this._infocliente = infocliente;
        }

        public async Task<GetClientesResponseIntermedio> GetClientePnByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            return await _infocliente.GetClientePnByDbcAsync(request, requestId);
        }

        public async Task<GetClienteResponseIntermedio> GetClientePnByIdcAsync(GetClientByIdcRequest request, string requestId)
        {
            return await _infocliente.GetClientePnByIdcAsync(request, requestId); 
        }

        public bool ValidarIdc(string tipoIdc)
        {
            return _infocliente.ValidarIdc(tipoIdc);
        }
    }
}
