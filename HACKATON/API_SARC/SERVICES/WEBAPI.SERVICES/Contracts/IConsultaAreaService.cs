using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.ConsultaArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IConsultaAreaService
    {
        Task<ServiceResponse<List<AreaRegistro>>> GetAreaByFuncionarioAsync(FuncionarioRequest request, string responseId);
        Task<ServiceResponse<List<AlertaArea>>> GetAlertaAreaRespuestaAsync(FuncionarioRespuestaRequest request, string responseId);
        Task<ServiceResponse<ConsultaArea>> GetConsultaAreaByCartaAsync(GetCasoDTORequest request, string requestId);
        Task<ServiceResponse<bool>> UpdateCasoRespuestaAreaAsync(UpdateCasoRespuestaArea casoRequest, string requestId);
    }
}
