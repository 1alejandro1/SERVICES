using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Asignacion;
using BCP.CROSS.MODELS.DTOs.Caso;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ICasoRepository
    {
        Task<InsertCasoResponse> InsertCaso(Caso casoRequest);
        Task<bool> CerrarCasoAsync(CerrarCasoRequest casoRequest);
        Task<bool> UpdateSolucionCasoAsync(UpdateCasoSolucionDTO casoRequest);
        Task<bool> UpdateSolucionCasoDateAsync(UpdateCasoSolucionDateDTO casoRequest);
        Task<bool> UpdateAsignacionCasoExpressAsync(UpdateAsignacionCasoDTO casoRequest);
        Task<bool> UpdateViaEnvioAsync(UpdateCasoDTOViaEnvio request);
        Task<CasoDTO> GetCasoAsync(string nroCaso);
        Task<CasoDTOCliente> GetCasoAllAsync(string nroCaso);
        Task<CasoDTOCompleto> GetCasoCompletoAsync(string nroCaso);
        Task<List<CasoDTOBaseAtencion>> GetCasoAtencionByEstadoAsync(string estado);
        Task<List<CasoDTOBaseAtencion>> GetCasoAtencionByEstadoUsuarioAsync(string estado, string funcionario);
        Task<List<GetCasoDTOByAnalistaResponse>> GetCasoByAnalistaAsync(string usuario,string estado);
        Task<List<GetCasoDTOByEstadoResponse>> GetCasoByEstadoAsync( string estado);
        Task<List<GetCasoResponse>> GetListCasoAsync(GetCasoRequest casoRequest);
        Task<bool> UpdateDevolucionCobroAsync(string NroCarta, string NroCuentaPR, int IdServiciosCanales,string centro, decimal Monto, string Moneda, string TipoFacturacion, int IndDevolucionCobro);
        Task<bool> UpdateSolucionCasoOrigenAsync(UpdateSolucionCasoDTOOrigenRequest casoRequest);
        Task<bool> UpdateCasoGrabarOrigenAsync(UpdateOrigenCasoDTORequest casoRequest);
        Task<string> GetReporteRegistroAsync(string NroCarta,string Usuario, string TipoReporte);
        Task<bool> InsertLogsCasoAsync(string nroCarta,string funMod);
        Task<List<Reclamos>> GetReclamoFuncionarioTipoAsync(string funcionario, string estado);
        Task<List<string>> GetReclamoFuncionarioTipoAlertaAsync(string funcionario, string estado);

        Task<List<CasoResumen>> GetCasoResumenByEstadoAsync(string estado);
        Task<bool> UpdateCasoEstadoComplejidadAsync(CasoComplejidad casoRequest);
        Task<bool> UpdateCasoRechazarAsync(UpdateCasoRechazarDTO casoRequest);
        Task<bool> UpdateCasoReaperturaAsync(UpdateCasoDTOEstado casoRequest);

        Task<List<CasoLog>> GetCasoLogsAllAsync(string nroCaso);     
         Task<bool> UpdateCasoRechazoAnalistaAsync(UpdateCasoRechazoAnalistaDTO request);
    }
}
