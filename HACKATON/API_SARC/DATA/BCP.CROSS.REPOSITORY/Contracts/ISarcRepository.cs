using BCP.CROSS.MODELS.Carta;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.MODELS.Especialidad;
using BCP.CROSS.MODELS.Sarc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ISarcRepository
    {
        Task<List<AreaResponse>> GetAreaAllAsync();
        Task<List<SucursalResponse>> GetSucursalAsync();
        Task<List<TipoSolucionResponse>> GetTipoSolucionAllAsync();        
        Task<List<ParametrosResponse>> GetParametrosSarcAsync(string lexico);
       
        Task<int> GetCountDevolucionesByAnalistaCuentaAsync(string NroCuenta, string Funcionario);
        Task<int> validarLimitesCuenta(string cuenta, string analista);      
        Task<List<Cargo>> GetCargoAllAsync();
        
        
        Task<bool> InsertClienteAsync(Cliente request);
        Task<List<ParametrosError>> GetErrorAllByTipoAsync(string tipo);
    }
}
