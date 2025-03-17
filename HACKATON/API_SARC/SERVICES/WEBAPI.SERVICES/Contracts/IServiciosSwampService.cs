using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.ServiciosSwamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IServiciosSwampService
    {
       
        Task<ServiceResponse<List<ParametrosResponse>>> GetParametrosSWAsync(string lexico, string requestId);
        Task<ServiceResponse<ServicioCanalResponse>> GetServicioCanalAsync(ServicioCanalIdRequest request, string requestId);
        Task<ServiceResponse<List<CuentaContableResponse>>> GetAllCuentaContableAsync(string requestId);
        Task<ServiceResponse<List<ServicioCanalResponse>>> GetServicioCanalByTipoAsync(ServicioCanalDebAboRequest request, string requestId);
        Task<ServiceResponse<List<Facturacion>>> GetFacturacionAllAsync(string requestId);       
        
    }
}
