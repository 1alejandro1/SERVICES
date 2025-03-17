using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.ServiciosSwamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IServiciosSwampRepository
    {
       
       
        Task<List<ParametrosResponse>> GetParametrosSWAsync(string lexico);
        Task<ServicioCanalResponse> GetServicioCanalByIdAsync(int idServicio);
        Task<List<ServicioCanalResponse>> GetServicioCanalByTipoAsync(bool debAbo);
        Task<List<CuentaContableResponse>> GetAllCuentaContableAsync();
        Task<decimal> ValidarImporte(decimal importe, string idProducto, string idServicio, string moneda, bool cobro);
        Task<List<Facturacion>> GetFacturacionAllAsync();
       
       
    }
}
