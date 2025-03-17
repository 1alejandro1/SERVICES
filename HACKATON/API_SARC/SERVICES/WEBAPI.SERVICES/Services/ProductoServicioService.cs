using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.ProductoServicio;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class ProductoServicioService: IProductoServicioService
    {
        private readonly IProductoServicioRepository _productoservicioRepository;
        public ProductoServicioService(IProductoServicioRepository productoservicioRepository)
        {
            this._productoservicioRepository = productoservicioRepository;
        }
        public async Task<ServiceResponse<List<Producto>>> GetProductoActivosAllAsync(string responseId)
        {
            var productos = await _productoservicioRepository.GetProductoActivosAllAsync();
            var response = new ServiceResponse<List<Producto>>()
            {
                Data = productos,
                Meta =
                {
                    Msj=productos!= null ? "Success" : "Producto no encontrado",
                    ResponseId=responseId,
                    StatusCode=productos!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<Producto>>> GetProductoAllAsync(string responseId)
        {
            var productos = await _productoservicioRepository.GetProductoAllAsync();
            var response = new ServiceResponse<List<Producto>>()
            {
                Data = productos,
                Meta =
                {
                    Msj=productos!= null ? "Success" : "Producto no encontrado",
                    ResponseId=responseId,
                    StatusCode=productos!= null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<List<ProductoServicio>>> GetProductoServicioAllAsync(string responseId)
        {
            var tipoSolucion = await _productoservicioRepository.GetProductoServicioAllAsync();
            var response = new ServiceResponse<List<ProductoServicio>>()
            {
                Data = tipoSolucion,
                Meta =
                {
                    Msj=tipoSolucion!= null ? "Success" : "Producto Servicio no encontrado",
                    ResponseId=responseId,
                    StatusCode=tipoSolucion!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ProductoByTipoEstadoResponse>>> GetServicioByProductoTipoEstadoAsync(TipoServicioEstadoRequest request, string responseId)
        {
            var producto = await _productoservicioRepository.GetProductoTipoEstadoAsync(request);
            var response = new ServiceResponse<List<ProductoByTipoEstadoResponse>>()
            {
                Data = producto,
                Meta =
                {
                    Msj=producto!= null ? "Success" : "Producto no encontrado",
                    ResponseId=responseId,
                    StatusCode=producto!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<ServicioByProductoTipoEstadoResponse>>> GetServicioByProductoTipoAsync(TipoServicioProductoRequest request, string responseId)
        {
            var servicios = await _productoservicioRepository.GetServicioByProductoTipoAsync(request.ProductoId, request.TipoServicio);
            var response = new ServiceResponse<List<ServicioByProductoTipoEstadoResponse>>()
            {
                Data = servicios,
                Meta =
                {
                    Msj=servicios!= null ? "Success" : "Servicios no encontrados",
                    ResponseId=responseId,
                    StatusCode=servicios!= null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<bool>> InsertProductoAsync(Producto request, string responseId)
        {
            var producto = await _productoservicioRepository.InsertProductoAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = producto,
                Meta =
                {
                    Msj=producto ? "Success" : "No se realizo el registro del producto",
                    ResponseId=responseId,
                    StatusCode=producto?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateProductoAsync(Producto request, string responseId)
        {
            var tipoCaso = await _productoservicioRepository.UpdateProductoAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = tipoCaso,
                Meta =
                {
                    Msj=tipoCaso ? "Success" : "No se realizo la modificacion del producto",
                    ResponseId=responseId,
                    StatusCode=tipoCaso?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> InsertServicioAsync(ServicioRegistroRequest request, string responseId)
        {
            var servicio = await _productoservicioRepository.InsertFullServicioAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = servicio,
                Meta =
                {
                    Msj=servicio ? "Success" : "No se realizo el registro del servicio",
                    ResponseId=responseId,
                    StatusCode=servicio?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateServicioAsync(ServicioModificacionRequest request, string responseId)
        {
            var servicio = await _productoservicioRepository.UpdateFullServicioAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = servicio,
                Meta =
                {
                    Msj=servicio ? "Success" : "No se realizo la modificacion del servicio",
                    ResponseId=responseId,
                    StatusCode=servicio?200:404
                }
            };
            return response;
        }
    }
}
