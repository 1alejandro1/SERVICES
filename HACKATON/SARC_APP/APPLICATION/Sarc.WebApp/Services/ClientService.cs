using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Client;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sarc.WebApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;
        private readonly SarcApiConnector _sarcApi;
        private readonly IAuthService _authService;

        public ClientService(IOptions<SarcApiSettings> sarcApiSettings, SarcApiConnector sarcApi, IAuthService authService)
        {
            _sarcApiSettings = sarcApiSettings;
            _sarcApi = sarcApi;
            _authService = authService;
        }

        public async Task<ServiceResponse<IEnumerable<GetClienteResponse>>> GetClientsByIdcAsync(GetClientByIdcRequest getClientByIdcRequest)
        {
            var user = await _authService.GetToken();
            getClientByIdcRequest.Funcionario = user.Matricula;
            var responseCliente = await _sarcApi.PostV2Async<IEnumerable<GetClienteResponse>, GetClientByIdcRequest>
                (_sarcApiSettings.Value.Clientes.GetClienteByIdc, getClientByIdcRequest);

            return responseCliente;
        }

        public async Task<ServiceResponse<IEnumerable<GetClienteResponse>>> GetClientsByDbcAsync(GetClientByDbcRequest getClientBydbcRequest)
        {
            var responseCliente = await _sarcApi.PostV2Async<IEnumerable<GetClienteResponse>, GetClientByDbcRequest>
                (_sarcApiSettings.Value.Clientes.GetClienteByDbc, getClientBydbcRequest);

            return responseCliente;
        }

        public async Task<ServiceResponse<IEnumerable<GetClienteResponse>>> GetClientsByIdcTestAsync(GetClientByIdcRequest getClientByIdcRequest)
        {
            ServiceResponse<IEnumerable<GetClienteResponse>> serviceResponse = new();
            serviceResponse = await Task.Run(() =>
                new ServiceResponse<IEnumerable<GetClienteResponse>>
                {
                    Data = new List<GetClienteResponse> {
                    new GetClienteResponse
                    {
                        Cleular1 =  "75672770",
                        Cleular2 = "75672771",
                        Direccion = "Balcon 1 C/Bibosi",
                        Direccion1 = string.Empty,
                        Direccion2 = string.Empty,
                        Email = "mvacac@bcp.com.bo",
                        Fax =   string.Empty,
                        Idc = "08462099QXX",
                        IdcExtension = "XX",
                        IdcTipo = "Q",
                        Materno = "VICKER",
                        NombreCompleto = "GONZALES VICKER RICHARD",
                        NombreEmpresa = string.Empty,
                        Nombres = "RICHARD RAFAEL",
                        NroIdc = "08462099",
                        PaternoRazonSocial = "GONZALES",
                        Telefono1 = string.Empty,
                        Telefono2 = string.Empty,
                        TipoCliente = "CL",
                        TipoPersona = "N"
                    },
                    new GetClienteResponse
                    {
                        Cleular1 =  "75672770",
                        Cleular2 = "75672771",
                        Direccion = "Balcon 1 C/Bibosi #54",
                        Direccion1 = string.Empty,
                        Direccion2 = string.Empty,
                        Email = "mvacac@bcp.com.bo",
                        Fax =   string.Empty,
                        Idc = "05638952QCH",
                        IdcExtension = "XX",
                        IdcTipo = "Q",
                        Materno = "CRUZ",
                        NombreCompleto = " VACA CRUZ MICAEL",
                        NombreEmpresa = string.Empty,
                        Nombres = "MICAEL",
                        NroIdc = "05638952",
                        PaternoRazonSocial = "VACA",
                        Telefono1 = string.Empty,
                        Telefono2 = string.Empty,
                        TipoCliente = "CL",
                        TipoPersona = "N"
                    }
                    },
                    Meta = new Meta
                    {
                        Msj = "PROCESO EJECUTADO CORRECTAMENTE",
                        StatusCode = 200,
                        ResponseId = "PR123545687"
                    }
                }
            );
            return serviceResponse;

        }
    }
}