using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.RepExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IRepExtService
    {
        Task<ServiceResponse<List<ValidaCuentaResponse>>> ValidarCuentaAsync(ValidaCuentaRequest request, string responseId);
        Task<ServiceResponse<List<GetClienteByCuentaResponse>>> GetDatosCuentaAsync(GetClienteByCuentaRequest request, string responseId);
    }
}
