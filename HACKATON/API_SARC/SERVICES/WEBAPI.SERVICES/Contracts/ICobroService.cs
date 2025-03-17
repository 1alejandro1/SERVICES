using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Cobro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ICobroService
    {
        Task<ServiceResponse<List<CobroProductoServicioResponse>>> ListaCobroByPRAsync(ProductoServicioRequest request, string requestId);
        Task<ServiceResponse<List<Cobro>>> GetCobroListAllAsync(string requestId);
        Task<ServiceResponse<bool>> InsertCobroAsync(CobroRegistro request, string requestId);
        Task<ServiceResponse<bool>> UpdateCobroAsync(CobroModificacion request, string requestId);
        Task<ServiceResponse<bool>> DeshabilitarCobroAsync(CobroDeshabilitar request, string requestId);
    }
}
