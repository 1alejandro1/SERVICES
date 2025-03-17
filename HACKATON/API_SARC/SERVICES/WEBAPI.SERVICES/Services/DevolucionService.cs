using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Devolucion;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class DevolucionService : IDevolucionService
    {
        private readonly IDevolucionRepository _devolucionRepository;
        public DevolucionService(IDevolucionRepository devolucionRequest)
        {
            this._devolucionRepository = devolucionRequest;
        }
        public async Task<ServiceResponse<List<DevolucionProductoServicioResponse>>> ListaDevolucionByPRAsync(ProductoServicioRequest request, string requestId)
        {
            var listaDevolucionPR = await _devolucionRepository.DevolucionByPRAsync(request.IdProducto, request.IdServicio);
            var response = new ServiceResponse<List<DevolucionProductoServicioResponse>>
            {
                Data = listaDevolucionPR,
                Meta = {
                    Msj = listaDevolucionPR != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = listaDevolucionPR != null ? 200 : 404
                }
            };

            return response;
        }
        public async Task<ServiceResponse<List<Devolucion>>> GetDevolucionListAllAsync(string requestId)
        {
            var devolucion = await _devolucionRepository.GetDevolucionListAllAsync();
            var response = new ServiceResponse<List<Devolucion>>
            {
                Data = devolucion,
                Meta = {
                    Msj = devolucion!=null ? "Success" : "No se encontraron devoluciones",
                    ResponseId = requestId,
                    StatusCode = devolucion!=null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateDevolucionAsync(DevolucionModificacion request, string requestId)
        {
            var devolucion = await _devolucionRepository.UpdateDevolucionAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = devolucion,
                Meta = {
                    Msj = devolucion? "Success" : "No se modifico el cobro",
                    ResponseId = requestId,
                    StatusCode = devolucion? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> DeshabilitarDevolucionAsync(DevolucionDeshabilitar request, string requestId)
        {
            var devolucion = await _devolucionRepository.DeshabilitarDevolucionAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = devolucion,
                Meta = {
                    Msj = devolucion? "Success" : "No se deshabilito la devolucion",
                    ResponseId = requestId,
                    StatusCode = devolucion? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> InsertDevolucionAsync(DevolucionRegistro request, string requestId)
        {
            var devolucion = await _devolucionRepository.InsertDevolucionAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = devolucion,
                Meta = {
                    Msj = devolucion? "Success" : "No se ingreso la devolucion",
                    ResponseId = requestId,
                    StatusCode = devolucion? 200 : 404
                }
            };

            return response;
        }
    }
}
