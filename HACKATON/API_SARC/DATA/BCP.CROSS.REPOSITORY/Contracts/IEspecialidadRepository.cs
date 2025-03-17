using BCP.CROSS.MODELS.Especialidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IEspecialidadRepository
    {
        Task<List<Especialidad>> GetTipoCasoAllAsync();
        Task<List<TipoCasoTiempo>> GetTipoCasoTiemposAllAsync();
        Task<bool> InsertTipoCasoAsync(TipoCasoRequest request);
        Task<bool> UpdateTipoCasoAsync(TipoCasoRequest request);
        Task<bool> InsertFuncionarioEspecialidadAsync(Especialidad request);
        Task<bool> DeleteFuncionarioEspecialidadAsync(string codFuncionario);
    }
}
