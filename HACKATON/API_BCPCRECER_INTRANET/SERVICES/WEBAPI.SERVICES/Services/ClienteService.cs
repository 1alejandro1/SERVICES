using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using BCP.CROSS.REPOSITORY.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IInfoclienteService _infoclienteService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IOptions<ApplicationSettings> _settings;

        public ClienteService(IInfoclienteService infoclienteService, IClienteRepository clienteRepository, IOptions<ApplicationSettings> settings)
        {
            _infoclienteService = infoclienteService;
            _clienteRepository = clienteRepository;
            _settings = settings;
        }
        

        public async Task<ServiceResponse<List<GetClienteResponse>>> GetClienteByIdcAsync(GetClientByIdcRequest request, string matricula, string requestId)
        {
            ServiceResponse<List<GetClienteResponse>>  response = new();
            response.Data = new List<GetClienteResponse>();
            var validaciones = _infoclienteService.ValidarIDC(request.TipoIdc);

            var esNatural = validaciones[0];//_settings.Value.InfoclientePn.TipoIdc.Contains( request.TipoIdc);
            var esJuridico = validaciones[1];//_settings.Value.InfoclientePj.TipoIdc.Contains(request.TipoIdc);

            if (esNatural)
            {
                var clientePn = await _infoclienteService.GetClientePnByIdcAsync(request, requestId);
                if (clientePn?.Data != null)
                {
                    response.Data.Add(clientePn.Data);
                }
                response.Meta = clientePn.Meta;
            }

            if (response.Data is null || response.Data?.Count == 0)
            {
                var clienteSarc = await _clienteRepository.GetClienteByIdcAsync(request);
                if (clienteSarc != null)
                {
                    response.Data.AddRange(clienteSarc);
                }
                response.Meta = new Meta
                {
                    Msj = clienteSarc == null ? "CLIENTE NO ENCONTRADO" : "SUCCESS",
                    ResponseId = requestId,
                    StatusCode = clienteSarc == null ? 404 : 200
                };
            }

            if (request.Extension.Trim().Equals("XX"))
            {
                var clienteSarcReclamos = await _clienteRepository.GetClienteByIdcReclamosAsync(request);

                if (clienteSarcReclamos?.Count > 0)
                {
                    foreach (var item in clienteSarcReclamos)
                    {
                        var existCliente = response.Data.Exists(x => x.NroIdc.Equals(item.NroIdc) 
                            && x.IdcExtension.Equals(item.IdcExtension) 
                            && x.PaternoRazonSocial.Equals(item.PaternoRazonSocial) 
                            && x.Nombres.Equals(item.Nombres));

                        if (!existCliente)
                        {
                            response.Data.Add(item);
                        }
                    }
                }
            }

            return response;
        }
        public async Task<ServiceResponse<List<GetClienteResponse>>> GetClienteByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            ServiceResponse<List<GetClienteResponse>> response = new();
            response.Data = new List<GetClienteResponse>();
            var clientePnDbc = await _infoclienteService.GetClientePnByDbcAsync(request, requestId);
            if (clientePnDbc.Data != null)
            {
                response.Data.AddRange(clientePnDbc.Data);
            }            
            response.Meta = clientePnDbc.Meta;
          

            return response;
        }
    }
}
