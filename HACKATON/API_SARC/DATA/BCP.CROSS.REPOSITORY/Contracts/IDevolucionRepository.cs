using BCP.CROSS.MODELS.Devolucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IDevolucionRepository
    {
        Task<List<DevolucionProductoServicioResponse>> DevolucionByPRAsync(string productoId, string servicioId);
        Task<List<Devolucion>> GetDevolucionListAllAsync();
        Task<bool> InsertDevolucionAsync(DevolucionRegistro request);
        Task<bool> UpdateDevolucionAsync(DevolucionModificacion request);
        Task<bool> DeshabilitarDevolucionAsync(DevolucionDeshabilitar request);
    }
}
