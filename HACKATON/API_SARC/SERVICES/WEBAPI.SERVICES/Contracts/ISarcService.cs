using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Sarc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ISarcService
    {
       
        Task<ServiceResponse<List<TipoSolucionResponse>>> GetTipoSolucionAllAsync(string responseId);
        
        Task<ServiceResponse<List<AreaResponse>>> GetAreaAllAsync(string responseId);        
        Task<ServiceResponse<List<SucursalResponse>>> GetSucursalAsync(string responseId);
        Task<ServiceResponse<List<ParametrosResponse>>> GetParametrosSarcAsync(string parametro, string responseId);

        Task<ServiceResponse<int>> GetDevolucionesByAnalistaCuentaAsync(ValorLexicos request, string responseId);
        Task<int> validarLimitesCuenta(string cuenta, string analista);           
        Task<ServiceResponse<List<Cargo>>> GetCargoAllAsync(string responseId);
        

        Task<ServiceResponse<bool>> InsertClienteAsync(Cliente request, string responseId);

        Task<ServiceResponse<List<ParametrosError>>> GetErrorAllByTipoAsync(string responseId);
    }
}
