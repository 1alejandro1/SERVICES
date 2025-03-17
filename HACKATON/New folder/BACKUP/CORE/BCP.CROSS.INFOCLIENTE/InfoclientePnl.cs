using BCP.CROSS.INFOCLIENTE.Services;
using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Cliente.Intermedio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.INFOCLIENTE
{
    public class InfoclientePnl : IInfoclientePn
    {
        public readonly IInfoclientePnServices _infoclienteService;

        public InfoclientePnl(IInfoclientePnServices infoclienteService)
        {
            _infoclienteService = infoclienteService;
        }

        public async Task<GetClientesResponseIntermedio> GetClientePnByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            return await _infoclienteService.GetClientePnByDbcAsync(request, requestId);
        }

        public async Task<GetClienteResponseIntermedio> GetClientePnByIdcAsync(GetClientByIdcRequest request, string requestId)
        {
            return await _infoclienteService.GetClientePnByIdcAsync(request,requestId);
        }

        public bool ValidarIdc(string tipoIdc)
        {
            return _infoclienteService.ValidarIdc(tipoIdc);
        }
    }
}
