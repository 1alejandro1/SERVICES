using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Helpers;
using BCP.CROSS.CONNECTORS;
using BCP.CROSS.MODELS.Client;
using BCP.CROSS.MODELS.Infocliente;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.SECRYPT;
using BCP.Framework.Logs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class InfoclienteService : IInfoclienteService
    {
        private readonly IInfoclientePnRepository _infoclientePn;
        public InfoclienteService(IInfoclientePnRepository infoclientePn)
        {
            _infoclientePn= infoclientePn;
        }

        public bool[] ValidarIDC(string tipo)
        {
            bool[] resultados = new bool[2];
            resultados[0] = _infoclientePn.ValidarIdc(tipo);
            return resultados;
        }
        public async Task<ServiceResponse<GetClienteResponse>> GetClientePnByIdcAsync(GetClientByIdcRequest request, string requestId)
        {
            ServiceResponse<GetClienteResponse> clienteResponse = new();
            var response = await _infoclientePn.GetClientePnByIdcAsync(request,requestId);

            if (response != null && response.exito)
            {
                clienteResponse.Data = response.cliente;
            }

            clienteResponse.Meta = new Meta
            {
                Msj = response.mensaje,
                ResponseId = response.operacion,
                StatusCode = response.exito ? 200 : 404
            };

            return clienteResponse;
        }

      

        #region #Busqueda_DBC

        public async Task<ServiceResponse<List<GetClienteResponse>>> GetClientePnByDbcAsync(GetClientByDbcRequest request, string requestId)
        {
            request.PaternoRazonSocial=request.PaternoRazonSocial != null ? request.PaternoRazonSocial.Trim().ToUpper() : "";
            request.Materno = request.Materno != null ? request.Materno.Trim().ToUpper() : "";
            request.Nombres = request.Nombres != null ? request.Nombres.Trim().ToUpper() : "";
            ServiceResponse<List<GetClienteResponse>> clienteDBCResponse = new();
            var response = await _infoclientePn.GetClientePnByDbcAsync(request, requestId); // PostAsync<ClientePnByDbcResponse, ClientePnByDbcRequest>(uri, requestPn);

            if (response != null && response.Exito)
            {
                clienteDBCResponse.Data = response.Clientes;
            }

            clienteDBCResponse.Meta = new Meta
            {
                Msj = response.Mensaje,
                ResponseId = response.Operacion,
                StatusCode = response.Exito ? 200 : 404
            };

            return clienteDBCResponse;
        }

       
        #endregion

    }
}
