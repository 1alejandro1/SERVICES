using BCP.CROSS.COMMON;
//====MODELS
using BCP.CROSS.MODELS.Reportes;
using AnalistaModel = BCP.CROSS.MODELS.Reportes.Analista;
using ASFIModel = BCP.CROSS.MODELS.Reportes.ASFI;
//==========
using BCP.CROSS.REPOSITORY.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;
using BCP.CROSS.MODELS.Reportes.CNR;
using BCP.CROSS.MODELS.Reportes.Requests;

namespace WEBAPI.SERVICES.Services
{

    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _reportesRepository;
        public ReportesService(IReportesRepository reportesRepository)
        {
            _reportesRepository = reportesRepository;
        }

        public async Task<ServiceResponse<List<AnalistaModel.CantidadCasosModel>>> AnalistaCantidadCasoService(ReporteRequest request, string RequestID)
        {
            List<AnalistaModel.CantidadCasosModel> ReportResponse = await _reportesRepository.GetAnalistaCantidadCaso(request);
            var response = new ServiceResponse<List<AnalistaModel.CantidadCasosModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<AnalistaModel.CasosSolucionadosModel>>> AnalistaCasosSolucionadosService(ReporteRequest request, string RequestID)
        {
            List<AnalistaModel.CasosSolucionadosModel> ReportResponse = await _reportesRepository.GetAnalistaCasosSolucionados(request);
            var response = new ServiceResponse<List<AnalistaModel.CasosSolucionadosModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<AnalistaModel.EspecialidadModel>>> AnalistaEspecialidadService(ReporteRequest request, string RequestID)
        {
            List<AnalistaModel.EspecialidadModel> ReportResponse = await _reportesRepository.GetAnalistaEspecialidad(request);
            var response = new ServiceResponse<List<AnalistaModel.EspecialidadModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ASFIModel.RegistradosModel>>> ASFIRegistradosService(ReporteRequest request, string RequestID)
        {
            List<ASFIModel.RegistradosModel> ReportResponse = await _reportesRepository.GetASFIRegistrados(request);
            var response = new ServiceResponse<List<ASFIModel.RegistradosModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ASFIModel.SolucionadosModel>>> ASFISolucionadosService(ReporteRequest request, string RequestID)
        {
            List<ASFIModel.SolucionadosModel> ReportResponse = await _reportesRepository.GetASFISolucionados(request);
            var response = new ServiceResponse<List<ASFIModel.SolucionadosModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<CapacidadEspecialidadModel>>> CapacidadEspecialidadService(ReporteRequest request, string RequestID)
        {
            List<CapacidadEspecialidadModel> ReportResponse = await _reportesRepository.GetCapacidadEspecialidad(request);
            var response = new ServiceResponse<List<CapacidadEspecialidadModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<CobrosDevolucionesModel>>> CobrosDevolucionesService(CobrosDevolucionesRequestModel request, string RequestID)
        {
            List<CobrosDevolucionesModel> ReportResponse = await _reportesRepository.GetCobrosDevoluciones(request);
            var response = new ServiceResponse<List<CobrosDevolucionesModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<DetalleModel>>> CRNDetalleService(CRNDetalleRequest request, string RequestID)
        {
            List<DetalleModel> ReportResponse = await _reportesRepository.GetCNRDetalle(request);
            var response = new ServiceResponse<List<DetalleModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<TotalModel>>> CRNTotalService(ReporteRequest request, string RequestID)
        {
            List<TotalModel> ReportResponse = await _reportesRepository.GetCNRTotal(request);
            var response = new ServiceResponse<List<TotalModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<DevolucionesATMPOSModel>>> DevolucionATMPOSService(ReporteRequest request, string RequestID)
        {
            List<DevolucionesATMPOSModel> ReportResponse = await _reportesRepository.GetDevolucionesATMPOS(request);
            var response = new ServiceResponse<List<DevolucionesATMPOSModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ExpedicionModel>>> ExpedicionService(ReporteRequest request, string RequestID)
        {
            List<ExpedicionModel> ReportResponse = await _reportesRepository.GetExpedicion(request);
            var response = new ServiceResponse<List<ExpedicionModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ReporteBaseModel>>> ReporteBaseService(ReporteRequest request, string RequestID)
        {
            List<ReporteBaseModel> ReportResponse = await _reportesRepository.GetReporteBase(request);
            var response = new ServiceResponse<List<ReporteBaseModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ReposicionTarjetaModel>>> ReposicionTarjetaService(ReporteRequest request, string RequestID)
        {
            List<ReposicionTarjetaModel> ReportResponse = await _reportesRepository.GetReposicionTarjeta(request);
            var response = new ServiceResponse<List<ReposicionTarjetaModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<TipoReclamoModel>>> TipoReclamoService(ReporteRequest request, string RequestID)
        {
            List<TipoReclamoModel> ReportResponse = await _reportesRepository.GetTipoReclamo(request);
            var response = new ServiceResponse<List<TipoReclamoModel>>
            {
                Data = ReportResponse,
                Meta = {
                    Msj = ReportResponse != null ? "Lista enviada" : "No se encuentra ninguna coincidencia.",
                    ResponseId = RequestID,
                    StatusCode = 200
                }
            };
            return response;
        }
    }
}
