using BCP.CROSS.MODELS.ProductoServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IProductoServicioRepository
    {
        Task<List<Producto>> GetProductoActivosAllAsync();
        Task<List<Producto>> GetProductoAllAsync();
        Task<List<ProductoServicio>> GetProductoServicioAllAsync();
        Task<List<ProductoByTipoEstadoResponse>> GetProductoTipoEstadoAsync(TipoServicioEstadoRequest request);
        Task<List<ServicioByProductoTipoEstadoResponse>> GetServicioByProductoTipoAsync(string producto, string tipoServicio);
        Task<bool> InsertProductoAsync(Producto request);
        Task<bool> UpdateProductoAsync(Producto request);
        //
        Task<bool> InsertFullServicioAsync(ServicioRegistroRequest request);
        Task<bool> UpdateFullServicioAsync(ServicioModificacionRequest request);
    }
}
