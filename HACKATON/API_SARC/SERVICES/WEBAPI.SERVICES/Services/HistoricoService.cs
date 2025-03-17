using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Historico;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class HistoricoService: IHistoricoService
    {
        private readonly IHistoricoRepository _historicoRepository;

        public HistoricoService(IHistoricoRepository casoRepository)
        {
            _historicoRepository = casoRepository;            
        }
        public async Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByIdcAllAsync(ClienteIdcRequest request, string requestId)
        {
            var responseCasos = await _historicoRepository.GetCasoHistoricoByIdcAllAsync(request.ClienteIdc, request.ClienteTipo, request.ClienteExtension);
            var response = new ServiceResponse<List<CasoDTOHistorico>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Casos historicos no Encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByIdcFechaAllAsync(ClienteIdcFechaRequest request, string requestId)
        {
            var responseCasos = await _historicoRepository.GetCasoHistoricoByIdcFechaAllAsync(request.ClienteIdc, request.ClienteTipo, request.ClienteExtension, request.FechaInicio, request.FechaFinal);
            var response = new ServiceResponse<List<CasoDTOHistorico>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Casos historicos no Encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByDbcAllAsync(ClienteDbcRequest request, string requestId)
        {
            var responseCasos = await _historicoRepository.GetCasoHistoricoByDbcAllAsync(request);
            var response = new ServiceResponse<List<CasoDTOHistorico>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Casos historicos no Encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<CasoDTOHistorico>>> GetCasoHistoricoByDbcFechaAllAsync(ClienteDbcFechaRequest request, string requestId)
        {
            var responseCasos = await _historicoRepository.GetCasoHistoricoByDbcFechaAllAsync(request);
            var response = new ServiceResponse<List<CasoDTOHistorico>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Casos historicos no Encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }
    }
}
