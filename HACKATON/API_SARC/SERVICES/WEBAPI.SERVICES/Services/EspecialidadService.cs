using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Especialidad;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class EspecialidadService: IEspecialidadService
    {
        private readonly IEspecialidadRepository _sarcRepository;
        public EspecialidadService(IEspecialidadRepository sarcRepository)
        {
            this._sarcRepository = sarcRepository;
        }
        public async Task<ServiceResponse<List<Especialidad>>> GetTipoCasoAllAsync(string responseId)
        {
            var tipoCaso = await _sarcRepository.GetTipoCasoAllAsync();
            var response = new ServiceResponse<List<Especialidad>>()
            {
                Data = tipoCaso,
                Meta =
                {
                    Msj=tipoCaso!= null ? "Success" : "No se obtuvo resultados de la busqueda",
                    ResponseId=responseId,
                    StatusCode=tipoCaso!= null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<List<TipoCasoTiempo>>> GetTipoCasoTiemposAllAsync(string responseId)
        {
            var tipoCaso = await _sarcRepository.GetTipoCasoTiemposAllAsync();
            var response = new ServiceResponse<List<TipoCasoTiempo>>()
            {
                Data = tipoCaso,
                Meta =
                {
                    Msj=tipoCaso!= null ? "Success" : "No se obtuvo resultados de la busqueda",
                    ResponseId=responseId,
                    StatusCode=tipoCaso!= null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<bool>> InsertTipoCasoAsync(TipoCasoRequest request, string responseId)
        {
            var tipoCaso = await _sarcRepository.InsertTipoCasoAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = tipoCaso,
                Meta =
                {
                    Msj=tipoCaso ? "Success" : "No se realizo el registro de la especialidad",
                    ResponseId=responseId,
                    StatusCode=tipoCaso?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<bool>> InsertFuncionarioEspecialidadAsync(Especialidad request, string responseId)
        {
            var carta = await _sarcRepository.InsertFuncionarioEspecialidadAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = carta,
                Meta =
                {
                    Msj=carta ? "Success" : "No se registro al funcionario en de especialidades",
                    ResponseId=responseId,
                    StatusCode=carta?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateTipoCasoAsync(TipoCasoRequest request, string responseId)
        {
            var tipoCaso = await _sarcRepository.UpdateTipoCasoAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = tipoCaso,
                Meta =
                {
                    Msj=tipoCaso ? "Success" : "No se realizo la modificacion de la especialidad",
                    ResponseId=responseId,
                    StatusCode=tipoCaso?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteFuncionarioEspecialidadAsync(FuncionarioEspecialidadRequest request, string responseId)
        {
            var carta = await _sarcRepository.DeleteFuncionarioEspecialidadAsync(request.Funcionario);
            var response = new ServiceResponse<bool>()
            {
                Data = carta,
                Meta =
                {
                    Msj=carta ? "Success" : "No se borro al funcionario del registro de especialidades",
                    ResponseId=responseId,
                    StatusCode=carta?200:404
                }
            };
            return response;
        }
    }
}
