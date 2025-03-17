using BCP.CROSS.MODELS.Reportes;
using BCP.CROSS.MODELS.Reportes.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnalistaModel = BCP.CROSS.MODELS.Reportes.Analista;
using ASFIModel = BCP.CROSS.MODELS.Reportes.ASFI;
using CNR = BCP.CROSS.MODELS.Reportes.CNR;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IReportesRepository
    {
        Task<List<ASFIModel.SolucionadosModel>> GetASFISolucionados(ReporteRequest Request);
        Task<List<ASFIModel.RegistradosModel>> GetASFIRegistrados(ReporteRequest Request);
        Task<List<AnalistaModel.CantidadCasosModel>> GetAnalistaCantidadCaso(ReporteRequest Request);
        Task<List<AnalistaModel.CasosSolucionadosModel>> GetAnalistaCasosSolucionados(ReporteRequest Request);
        Task<List<AnalistaModel.EspecialidadModel>> GetAnalistaEspecialidad(ReporteRequest Request);
        Task<List<ReporteBaseModel>> GetReporteBase(ReporteRequest Request);
        Task<List<CapacidadEspecialidadModel>> GetCapacidadEspecialidad(ReporteRequest Request);
        Task<List<CNR.TotalModel>> GetCNRTotal(ReporteRequest Request);
        Task<List<CNR.DetalleModel>> GetCNRDetalle(CRNDetalleRequest Request);
        Task<List<TipoReclamoModel>> GetTipoReclamo(ReporteRequest Request);
        Task<List<ReposicionTarjetaModel>> GetReposicionTarjeta(ReporteRequest Request);
        Task<List<DevolucionesATMPOSModel>> GetDevolucionesATMPOS(ReporteRequest Request);
        Task<List<ExpedicionModel>> GetExpedicion(ReporteRequest Request);
        Task<List<CobrosDevolucionesModel>> GetCobrosDevoluciones(CobrosDevolucionesRequestModel Request);
    }
}
