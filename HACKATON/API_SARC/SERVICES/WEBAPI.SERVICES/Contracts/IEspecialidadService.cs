using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Especialidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IEspecialidadService
    {
        Task<ServiceResponse<List<Especialidad>>> GetTipoCasoAllAsync(string responseId);
        Task<ServiceResponse<List<TipoCasoTiempo>>> GetTipoCasoTiemposAllAsync(string responseId);
        Task<ServiceResponse<bool>> InsertTipoCasoAsync(TipoCasoRequest request, string responseId);
        Task<ServiceResponse<bool>> UpdateTipoCasoAsync(TipoCasoRequest request, string responseId);
        Task<ServiceResponse<bool>> InsertFuncionarioEspecialidadAsync(Especialidad request, string responseId);
        Task<ServiceResponse<bool>> DeleteFuncionarioEspecialidadAsync(FuncionarioEspecialidadRequest request, string responseId);
    }
}
