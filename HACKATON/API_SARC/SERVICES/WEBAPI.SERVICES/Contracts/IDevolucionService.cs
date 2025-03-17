using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Devolucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IDevolucionService
    {
        Task<ServiceResponse<List<DevolucionProductoServicioResponse>>> ListaDevolucionByPRAsync(ProductoServicioRequest request, string requestId);
        Task<ServiceResponse<List<Devolucion>>> GetDevolucionListAllAsync(string requestId);
        Task<ServiceResponse<bool>> InsertDevolucionAsync(DevolucionRegistro request, string requestId);
        Task<ServiceResponse<bool>> UpdateDevolucionAsync(DevolucionModificacion request, string requestId);
        Task<ServiceResponse<bool>> DeshabilitarDevolucionAsync(DevolucionDeshabilitar request, string requestId);
    }
}
