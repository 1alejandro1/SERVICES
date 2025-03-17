using BCP.CROSS.INFOCLIENTE;
using BCP.CROSS.INFOCLIENTEJURIDICO;
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
    public class InfoclientePjRepository: IInfoclientePjRepository
    {
        private readonly InfoclientePjl _infocliente;

        public InfoclientePjRepository(InfoclientePjl infocliente)
        {
            _infocliente = infocliente;
        }

        public async Task<GetClientesResponseIntermedio> GetClientePjByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            return await _infocliente.GetClientePjByDbcAsync(request, requestId);   
        }

        public async Task<GetClientesResponseIntermedio> GetClientePjByIdcAsync(GetClientByIdcRequest request, string matricula, string requestId)
        {
            return await _infocliente.GetClientePjByIdcAsync(request, matricula, requestId);   
        }

        public bool ValidarIdc(string tipoIdc)
        {
            return _infocliente.ValidarIdc(tipoIdc);
        }
    }
}
