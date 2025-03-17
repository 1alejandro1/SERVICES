using BCP.CROSS.MODELS.RepExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IRepExtRepository
    {
        Task<List<ValidaCuentaResponse>> ValidarCuentaAsync(ValidaCuentaRequest request);
        Task<List<GetClienteByCuentaResponse>> GetDatosCuentaAsync(string cuentaRepExt);
    }
}
