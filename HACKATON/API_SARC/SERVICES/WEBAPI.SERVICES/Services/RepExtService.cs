using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.RepExt;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class RepExtService : IRepExtService
    {
        private IRepExtRepository _repExtRepository;
        public RepExtService(IRepExtRepository repExtRepository)
        {
            this._repExtRepository = repExtRepository;
        }

        public async Task<ServiceResponse<List<GetClienteByCuentaResponse>>> GetDatosCuentaAsync(GetClienteByCuentaRequest request, string requestId)
        {
            var validarCuenta = await _repExtRepository.GetDatosCuentaAsync(request.CuentaRepExt);
            var response = new ServiceResponse<List<GetClienteByCuentaResponse>>
            {
                Data = validarCuenta,
                Meta = {
                    Msj = validarCuenta != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = validarCuenta != null ? 200 : 404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ValidaCuentaResponse>>> ValidarCuentaAsync(ValidaCuentaRequest request, string requestId)
        {
            var validarCuenta = await _repExtRepository.ValidarCuentaAsync(request);
            var response = new ServiceResponse<List<ValidaCuentaResponse>>
            {
                Data = validarCuenta,
                Meta = {
                    Msj = validarCuenta != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = validarCuenta != null ? 200 : 404
                }
            };

            return response;
        }
    }
}
