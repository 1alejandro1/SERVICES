using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.Sarc;
using BCP.CROSS.REPOSITORY.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class SarcService : ISarcService
    {
        private readonly ISarcRepository _sarcRepository;
        public SarcService(ISarcRepository sarcRepository)
        {
            this._sarcRepository = sarcRepository;
        }

        public async Task<ServiceResponse<List<AreaResponse>>> GetAreaAllAsync(string responseId)
        {
            var areas = await _sarcRepository.GetAreaAllAsync();
            var response = new ServiceResponse<List<AreaResponse>>()
            {
                Data = areas,
                Meta =
                {
                    Msj=areas!= null ? "Success" : "Ocurrio un error al cargar la lista de areas",
                    ResponseId=responseId,
                    StatusCode=areas!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ParametrosResponse>>> GetParametrosSarcAsync(string parametro, string responseId)
        {
            var productos = await _sarcRepository.GetParametrosSarcAsync(parametro);
            var response = new ServiceResponse<List<ParametrosResponse>>()
            {
                Data = productos,
                Meta =
                {
                    Msj=productos!= null ? "Success" : "Parametro no encontrado",
                    ResponseId=responseId,
                    StatusCode=productos!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<TipoSolucionResponse>>> GetTipoSolucionAllAsync(string responseId)
        {
            var tipoSolucion = await _sarcRepository.GetTipoSolucionAllAsync ();
            var response = new ServiceResponse<List<TipoSolucionResponse>>()
            {
                Data = tipoSolucion,
                Meta =
                {
                    Msj=tipoSolucion!= null ? "Success" : "Tipo Solucion no encontrado",
                    ResponseId=responseId,
                    StatusCode=tipoSolucion!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<SucursalResponse>>> GetSucursalAsync(string responseId)
        {
            var areas = await _sarcRepository.GetSucursalAsync();
            var response = new ServiceResponse<List<SucursalResponse>>()
            {
                Data = areas,
                Meta =
                {
                    Msj=areas!= null ? "Success" : "Ocurrio un error al cargar la lista de Sucursales",
                    ResponseId=responseId,
                    StatusCode=areas!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<int>> GetDevolucionesByAnalistaCuentaAsync(ValorLexicos request, string responseId)
        {
            string NroCuenta = string.Empty;
            string Funcionario=string.Empty;
            if(request.Lexico.ToLower().Equals("cuenta"))
            {
                NroCuenta = request.Valor;
            }
            else
            {
                Funcionario = request.Valor;
            }
            var devoluciones = await _sarcRepository.GetCountDevolucionesByAnalistaCuentaAsync(NroCuenta,Funcionario);
            var response = new ServiceResponse<int>()
            {
                Data = devoluciones,
                Meta =
                {
                    Msj="Success" ,
                    ResponseId=responseId,
                    StatusCode=200
                }
            };
            return response;
        }
        #region validar
        public async Task<int> validarLimitesCuenta(string cuenta, string analista)
        {
            return await _sarcRepository.validarLimitesCuenta(cuenta,analista);
        }
        #endregion          
        public async Task<ServiceResponse<List<Cargo>>> GetCargoAllAsync(string responseId)
        {
            var cargo = await _sarcRepository.GetCargoAllAsync();
            var response = new ServiceResponse<List<Cargo>>()
            {
                Data = cargo,
                Meta =
                {
                    Msj=cargo!=null ? "Success" : "No se encontraron registros",
                    ResponseId=responseId,
                    StatusCode=cargo!=null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<bool>> InsertClienteAsync(Cliente request, string responseId)
        {
            var cliente = await _sarcRepository.InsertClienteAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = cliente,
                Meta =
                {
                    Msj=cliente ? "Success" : "No se registro al cliente en SARC",
                    ResponseId=responseId,
                    StatusCode=cliente?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ParametrosError>>> GetErrorAllByTipoAsync(string responseId)
        {
            var error = await _sarcRepository.GetErrorAllByTipoAsync("1");
            var response = new ServiceResponse<List<ParametrosError>>()
            {
                Data = error,
                Meta =
                {
                    Msj=error!=null ? "Success" : "No se encontraron registros",
                    ResponseId=responseId,
                    StatusCode=error!=null?200:404
                }
            };
            return response;
        }
    }
}
