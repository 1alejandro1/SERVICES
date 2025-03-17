using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Reportes;
using BCP.CROSS.MODELS.Reportes.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalistaModel = BCP.CROSS.MODELS.Reportes.Analista;
using ASFIModel = BCP.CROSS.MODELS.Reportes.ASFI;
using CNR = BCP.CROSS.MODELS.Reportes.CNR;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IReportesService
    {
        Task<ServiceResponse<List<ASFIModel.SolucionadosModel>>> ASFISolucionadosService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<ASFIModel.RegistradosModel>>> ASFIRegistradosService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<AnalistaModel.CantidadCasosModel>>> AnalistaCantidadCasoService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<AnalistaModel.CasosSolucionadosModel>>> AnalistaCasosSolucionadosService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<AnalistaModel.EspecialidadModel>>> AnalistaEspecialidadService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<ReporteBaseModel>>> ReporteBaseService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<CapacidadEspecialidadModel>>> CapacidadEspecialidadService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<CNR.TotalModel>>> CRNTotalService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<CNR.DetalleModel>>> CRNDetalleService(CRNDetalleRequest request, string RequestID);
        Task<ServiceResponse<List<TipoReclamoModel>>> TipoReclamoService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<ReposicionTarjetaModel>>> ReposicionTarjetaService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<DevolucionesATMPOSModel>>> DevolucionATMPOSService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<ExpedicionModel>>> ExpedicionService(ReporteRequest request, string RequestID);
        Task<ServiceResponse<List<CobrosDevolucionesModel>>> CobrosDevolucionesService(CobrosDevolucionesRequestModel request, string RequestID);
    }
}
