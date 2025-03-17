using BCP.CROSS.MODELS.Tarifario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ITarifarioRepository
    {

        Task<List<Tarifario>> GetAllTarifarioAsync();
        Task<bool> InsertTarifarioAsync(TarifarioRegistro request);
        Task<bool> UpdateTarifarioAsync(TarifarioModificacion request);
        Task<bool> DeshabilitarTarifarioAsync(TarifiarioDeshabilitar request);
    }
}
