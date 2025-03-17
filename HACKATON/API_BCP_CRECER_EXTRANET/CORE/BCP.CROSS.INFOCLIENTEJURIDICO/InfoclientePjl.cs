using BCP.CROSS.INFOCLIENTEJURIDICO.Services;
using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Cliente.Intermedio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.INFOCLIENTEJURIDICO
{
    public class InfoclientePjl : IInfoclientePj
    {
        private readonly IInfoclientePjServices _infocliente;
        public InfoclientePjl(IInfoclientePjServices infocliente)
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
