using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.ProductoServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IProductoServicioService
    {
        Task<ServiceResponse<List<Producto>>> GetProductoActivosAllAsync(string responseId);
        Task<ServiceResponse<List<Producto>>> GetProductoAllAsync(string responseId);
        Task<ServiceResponse<List<ProductoServicio>>> GetProductoServicioAllAsync(string responseId);
        Task<ServiceResponse<List<ProductoByTipoEstadoResponse>>> GetServicioByProductoTipoEstadoAsync(TipoServicioEstadoRequest request, string responseId);
        Task<ServiceResponse<bool>> InsertProductoAsync(Producto request, string responseId);
        Task<ServiceResponse<bool>> UpdateProductoAsync(Producto request, string responseId);
        Task<ServiceResponse<List<ServicioByProductoTipoEstadoResponse>>> GetServicioByProductoTipoAsync(TipoServicioProductoRequest request, string responseId);
        Task<ServiceResponse<bool>> InsertServicioAsync(ServicioRegistroRequest request, string responseId);
        Task<ServiceResponse<bool>> UpdateServicioAsync(ServicioModificacionRequest request, string responseId);
    }
}
