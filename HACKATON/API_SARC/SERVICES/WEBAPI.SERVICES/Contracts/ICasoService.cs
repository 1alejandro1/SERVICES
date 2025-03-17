using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Asignacion;
using BCP.CROSS.MODELS.DTOs.Caso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ICasoService
    {
        Task<ServiceResponse<InsertCasoResponse>> AddCasoAsync(CreateCasoDTO casoRequest, string requestId);
        Task<ServiceResponse<InsertCasoResponseV2>> AddCasoAsyncV2(CreateCasoDTOV2 casoRequest, string requestId, bool debAbo = false);
        Task<ServiceResponse<bool>> CerrarCasoAsync(CerrarCasoRequest casoRequest, string requestId,bool escribirLogs=true);
        Task<ServiceResponse<bool>> UpdateSolucionCasoDateAsync(UpdateCasoSolucionDateDTO casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateSolucionCasoAsync(UpdateCasoSolucionDTO casoRequest, string requestId, bool escribirLogs = true);
        Task<ServiceResponse<bool>> UpdateAsignacionCasoExpressAsync(UpdateAsignacionCasoDTO casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateViaEnvioAsync(UpdateCasoDTOViaEnvio request, string requestId);
        Task<ServiceResponse<CasoDTO>> GetCasoAsync(GetCasoDTO casoRequest, string requestId);
        Task<ServiceResponse<CasoDTOCliente>> GetCasoAllAsync(GetCasoDTO casoRequest, string requestId);
        Task<ServiceResponse<CasoDTOCompleto>> GetCasoCompletoAsync(GetCasoDTO casoRequest, string requestId);
        Task<ServiceResponse<List<CasoDTOBaseAtencion>>> GetCasoAtencionByEstadoAsync(EstadoCasoRequest casoRequest, string requestId);
        Task<ServiceResponse<List<CasoDTOBaseAtencion>>> GetCasoAtencionByEstadoUsuarioAsync(EstadoCasoFuncionarioRequest casoRequest, string requestId);

        Task<ServiceResponse<List<GetCasoResponse>>> GetListCasoAsync(GetCasoRequest casoRequest, string requestId);
        Task<ServiceResponse<List<GetCasoDTOByEstadoResponse>>> GetCasoByEstadoAsync(GetCasoDTOByEstadoRequest casoRequest, string requestId);
        Task<ServiceResponse<List<GetCasoDTOByAnalistaResponse>>> GetCasoByAnalistaAsync(GetCasoDTOByAnalistaRequest casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateDevolucionCobroAsync(UpdateDevolucionCobroRequest casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateSolucionCasoOrigenAsync(UpdateSolucionCasoDTOOrigenRequest casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateCasoGrabarOrigenAsync(UpdateOrigenCasoDTORequest casoRequest, string requestId);
        Task<ServiceResponse<string>> GetReporteRegistroAsync(DatosReporteRequest casoRequest, string requestId);
        Task<ServiceResponse<InsertCasoResponseV2>> CierraCasoExpress(FlujoCasoExpressDTORequest request, string requestId);
        //
        
        
        
        Task<ServiceResponse<List<CasoResumen>>> GetCasoResumenByEstadoAsync(EstadoCasoRequest casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateCasoEstadoComplejidadAsync(CasoComplejidad casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateCasoRechazarAsync(UpdateCasoRechazarDTO casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateCasoReaperturaAsync(UpdateCasoDTOEstado casoRequest, string requestId);
        Task<ServiceResponse<List<Reclamos>>> GetReclamoFuncionarioTipoAsync(AlertaAnalistaRequest request, string responseId);
        Task<ServiceResponse<List<string>>> GetReclamoFuncionarioTipoAlertaAsync(MatriculaFuncionario request, string responseId);
        Task<ServiceResponse<List<CasoLog>>> GetCasoLogsAllAsync(GetCasoDTO casoRequest, string requestId);
        Task<ServiceResponse<bool>> UpdateCasoRechazoAnalistaAsync(UpdateCasoRechazoAnalistaDTO request, string requestId);

    }
}
