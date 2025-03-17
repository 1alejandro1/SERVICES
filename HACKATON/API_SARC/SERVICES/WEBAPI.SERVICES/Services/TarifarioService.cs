using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Tarifario;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class TarifarioService: WEBAPI.SERVICES.Contracts.ITarifarioService
    {
        private readonly ITarifarioRepository _tarifarioRepository;
        public TarifarioService(ITarifarioRepository tarifarioRepository)
        {
            this._tarifarioRepository = tarifarioRepository;
        }
        public async Task<ServiceResponse<List<Tarifario>>> GetAllTarifarioAsync(string requestId)
        {
            var listaTarifario = await _tarifarioRepository.GetAllTarifarioAsync();
            var response = new ServiceResponse<List<Tarifario>>
            {
                Data = listaTarifario,
                Meta = {
                    Msj = listaTarifario != null ? "Success" : "No se pudo recuperar la lista de tarifarios",
                    ResponseId = requestId,
                    StatusCode = listaTarifario != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> InsertTarifarioAsync(TarifarioRegistro request, string requestId)
        {
            var tarifario = await _tarifarioRepository.InsertTarifarioAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = tarifario,
                Meta = {
                    Msj = tarifario ? "Success" : "No se guardo el registro de tarifario",
                    ResponseId = requestId,
                    StatusCode = tarifario ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateTarifarioAsync(TarifarioModificacion request, string requestId)
        {
            var tarifario = await _tarifarioRepository.UpdateTarifarioAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = tarifario,
                Meta = {
                    Msj = tarifario ? "Success" : "No se guardo el cambio de tarifario",
                    ResponseId = requestId,
                    StatusCode = tarifario ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> DeshabilitarTarifarioAsync(TarifiarioDeshabilitar request, string requestId)
        {
            var tarifario = await _tarifarioRepository.DeshabilitarTarifarioAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = tarifario,
                Meta = {
                    Msj = tarifario ? "Success" : "No se deshabilito tarifario",
                    ResponseId = requestId,
                    StatusCode = tarifario ? 200 : 404
                }
            };

            return response;
        }
    }
}
