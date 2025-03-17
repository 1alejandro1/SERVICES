using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ITarifarioService
    {
        Task<ServiceResponse<List<Tarifario>>> GetAllTarifarioAsync(string requestId);
        Task<ServiceResponse<bool>> InsertTarifarioAsync(TarifarioRegistro request, string requestId);
        Task<ServiceResponse<bool>> UpdateTarifarioAsync(TarifarioModificacion request, string requestId);
        Task<ServiceResponse<bool>> DeshabilitarTarifarioAsync(TarifiarioDeshabilitar request, string requestId);
    }
}
