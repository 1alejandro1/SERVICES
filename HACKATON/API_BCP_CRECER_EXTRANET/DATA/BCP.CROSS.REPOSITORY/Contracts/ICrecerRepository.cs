using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Crecer;
using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.CROSS.MODELS.DTOs.Crecer;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ICrecerRepository
    {
        Task<List<CategoriasResponse>> GetCategoriasAsync();
        Task<List<ObtieneEmpresasByCategoriaCiudadResponse>> ObtieneEmpresasByCategoriaCiudadAsync(ObtieneEmpresasByCategoriaCiudadRequest request);
    }
}
