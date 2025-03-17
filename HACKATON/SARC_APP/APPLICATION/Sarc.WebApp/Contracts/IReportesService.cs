using BCP.CROSS.MODELS;
using ASFI=BCP.CROSS.MODELS.Reportes.ASFI;
using Analista = BCP.CROSS.MODELS.Reportes.Analista;
using CNR = BCP.CROSS.MODELS.Reportes.CNR;
using Req = BCP.CROSS.MODELS.Reportes.Requests;
using Sarc.WebApp.Models.Reportes;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCP.CROSS.MODELS.Reportes;
using BCP.CROSS.MODELS.Carta;

namespace Sarc.WebApp.Contracts
{
    public interface IReportesService
    {
        Task<ServiceResponse<List<ASFI.SolucionadosModel>>> GetASFISoluciones(ReportesFormViewModel Request);
        Task<ServiceResponse<List<ASFI.RegistradosModel>>> GetASFIRegistrados(ReportesFormViewModel Request);
        Task<ServiceResponse<List<Analista.CantidadCasosModel>>> GetAnalistaCantidadCasos(ReportesFormViewModel Request);
        Task<ServiceResponse<List<Analista.CasosSolucionadosModel>>> GetAnalistaCasosSolucionados(ReportesFormViewModel Request);
        Task<ServiceResponse<List<Analista.EspecialidadModel>>> GetAnalistaEspecialidad(ReportesFormViewModel Request);
        Task<ServiceResponse<List<CapacidadEspecialidadModel>>> GetCapacidadEspecialidad(ReportesFormViewModel Request);
        Task<ServiceResponse<List<ReporteBaseModel>>> GetReporteBase(ReportesFormViewModel Request);
        Task<ServiceResponse<List<CNR.TotalModel>>> GetCNRTotal(ReportesFormViewModel Request);
        Task<ServiceResponse<List<CNR.DetalleModel>>> GetCNRDetalle(ReportesFormViewModel Request);
        Task<ServiceResponse<List<TipoReclamoModel>>> GetTipoReclamo(ReportesFormViewModel Request);
        Task<ServiceResponse<List<ReposicionTarjetaModel>>> GetReposicionTarjeta(ReportesFormViewModel Request);
        Task<ServiceResponse<List<DevolucionesATMPOSModel>>> GetDevolucionATMPOS(ReportesFormViewModel Request);
        Task<ServiceResponse<List<ExpedicionModel>>> GetExpedicion(ReportesFormViewModel Request);
        Task<ServiceResponse<List<CobrosDevolucionesModel>>> GetCobrosDevoluciones(CobrosDevolucionesViewModel Request);
        Task<ServiceResponse<string>> GetCartaFile(CartaFileRequest Request);

    }
}
