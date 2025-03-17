using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Crecer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.CROSS.MODELS.DTOs.Crecer;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ICrecerService
    {
        Task<ServiceResponse<List<CategoriasResponse>>> GetCategoriasAsync(string responseId);
        Task<ServiceResponse<List<ObtieneEmpresasByCategoriaCiudadResponse>>> ObtieneEmpresasByCategoriaCiudadAsync(ObtieneEmpresasByCategoriaCiudadRequest request, string requestId);
    }
}