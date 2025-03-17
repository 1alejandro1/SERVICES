using BCP.CROSS.MODELS.Cobro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ICobroRepository
    {
        Task<List<CobroProductoServicioResponse>> CobroByPRAsync(string productoId, string servicioId);
        Task<List<Cobro>> GetCobroListAllAsync();
        Task<bool> InsertCobroAsync(CobroRegistro request);
        Task<bool> UpdateCobroAsync(CobroModificacion request);
        Task<bool> DeshabilitarCobroAsync(CobroDeshabilitar request);
    }
}
