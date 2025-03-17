using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.ConsultaArea;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class ConsultaAreaService: IConsultaAreaService
    {
        private readonly IConsultaAreaRepository _sarcRepository;
        public ConsultaAreaService(IConsultaAreaRepository sarcRepository)
        {
            this._sarcRepository = sarcRepository;
        }
        public async Task<ServiceResponse<List<AreaRegistro>>> GetAreaByFuncionarioAsync(FuncionarioRequest request, string responseId)
        {
            var areas = await _sarcRepository.GetAreaByFuncionarioAsync(request.Funcionario);
            var response = new ServiceResponse<List<AreaRegistro>>()
            {
                Data = areas,
                Meta =
                {
                    Msj=areas!= null ? "Success" : "El funcionario no presenta casos pendientes",
                    ResponseId=responseId,
                    StatusCode=areas!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<AlertaArea>>> GetAlertaAreaRespuestaAsync(FuncionarioRespuestaRequest request, string responseId)
        {
            var areas = await _sarcRepository.GetAlertaAreaRespuestaAsync(request.Funcionario, request.Respuesta);
            var response = new ServiceResponse<List<AlertaArea>>()
            {
                Data = areas,
                Meta =
                {
                    Msj=areas!= null ? "Success" : "No se obtuvo resultados de la busqueda",
                    ResponseId=responseId,
                    StatusCode=areas!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<ConsultaArea>> GetConsultaAreaByCartaAsync(GetCasoDTORequest request, string requestId)
        {
            var responseArea = await _sarcRepository.GetConsultaAreaByCartaAsync(request.NroCaso);
            var response = new ServiceResponse<ConsultaArea>
            {
                Data = responseArea,
                Meta = {
                    Msj = responseArea != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseArea != null ? 200 : 404
                }
            };

            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateCasoRespuestaAreaAsync(UpdateCasoRespuestaArea casoRequest, string requestId)
        {
            var responseArea = await _sarcRepository.UpdateCasoRespuestaAreaAsync(casoRequest);
            var response = new ServiceResponse<bool>
            {
                Data = responseArea,
                Meta = {
                    Msj = responseArea? "Success" : "No se registro la respuesta del Area",
                    ResponseId = requestId,
                    StatusCode = responseArea ? 200 : 404
                }
            };

            return response;
        }
    }
}
