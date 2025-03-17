using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.ServiciosSwamp;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class ServiciosSwampService : IServiciosSwampService
    {
        private readonly IServiciosSwampRepository _srvSwamp;
        public ServiciosSwampService(IServiciosSwampRepository srvSwamp)
        {
            this._srvSwamp= srvSwamp;
        }

        

        public async Task<ServiceResponse<List<CuentaContableResponse>>> GetAllCuentaContableAsync(string requestId)
        {
            var getCuentasContables = await _srvSwamp.GetAllCuentaContableAsync();
            var response = new ServiceResponse<List<CuentaContableResponse>>
            {
                Data = getCuentasContables,
                Meta = {
                    Msj = getCuentasContables != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = getCuentasContables != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<ParametrosResponse>>> GetParametrosSWAsync(string lexico, string requestId)
        {
            var getParametros = await _srvSwamp.GetParametrosSWAsync(lexico);
            var response = new ServiceResponse<List<ParametrosResponse>>
            {
                Data = getParametros,
                Meta = {
                    Msj = getParametros != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = getParametros != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<ServicioCanalResponse>> GetServicioCanalAsync(ServicioCanalIdRequest request, string requestId)
        {
            var servicioCanal = await _srvSwamp.GetServicioCanalByIdAsync(request.IdServicioCanal);
            var response = new ServiceResponse<ServicioCanalResponse>
            {
                Data = servicioCanal,
                Meta = {
                    Msj = servicioCanal != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = servicioCanal != null ? 200 : 404
                }
            };

            return response;
        }

        

        public async Task<ServiceResponse<List<ServicioCanalResponse>>> GetServicioCanalByTipoAsync(ServicioCanalDebAboRequest request, string requestId)
        {
            var servicioCanal = await _srvSwamp.GetServicioCanalByTipoAsync(request.DebitoAbono);
            var response = new ServiceResponse<List<ServicioCanalResponse>>
            {
                Data = servicioCanal,
                Meta = {
                    Msj = servicioCanal != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = servicioCanal != null ? 200 : 404
                }
            };

            return response;
        }

        

        public async Task<ServiceResponse<List<Facturacion>>> GetFacturacionAllAsync(string requestId)
        {
            var facturacion = await _srvSwamp.GetFacturacionAllAsync();
            var response = new ServiceResponse<List<Facturacion>>
            {
                Data = facturacion,
                Meta = {
                    Msj = facturacion!=null ? "Success" : "No se deshabilito tarifario",
                    ResponseId = requestId,
                    StatusCode = facturacion!=null ? 200 : 404
                }
            };

            return response;
        }      
    }
}
