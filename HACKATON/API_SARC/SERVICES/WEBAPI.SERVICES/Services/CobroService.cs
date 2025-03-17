using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Cobro;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class CobroService: ICobroService 
    {
        private readonly ICobroRepository _cobroRepository;
        public CobroService(ICobroRepository cobroRepository)
        {
            this._cobroRepository = cobroRepository;
        }
        public async Task<ServiceResponse<List<CobroProductoServicioResponse>>> ListaCobroByPRAsync(ProductoServicioRequest request, string requestId)
        {
            var listaCobroPR = await _cobroRepository.CobroByPRAsync(request.IdProducto, request.IdServicio);
            var response = new ServiceResponse<List<CobroProductoServicioResponse>>
            {
                Data = listaCobroPR,
                Meta = {
                    Msj = listaCobroPR != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = listaCobroPR != null ? 200 : 404
                }
            };

            return response;
        }
        public async Task<ServiceResponse<List<Cobro>>> GetCobroListAllAsync(string requestId)
        {
            var cobro = await _cobroRepository.GetCobroListAllAsync();
            var response = new ServiceResponse<List<Cobro>>
            {
                Data = cobro,
                Meta = {
                    Msj = cobro!=null? "Success" : "No se encontraron registros de cobros",
                    ResponseId = requestId,
                    StatusCode = cobro!=null? 200 : 404
                }
            };

            return response;
        }
        public async Task<ServiceResponse<bool>> InsertCobroAsync(CobroRegistro request, string requestId)
        {
            var cobro = await _cobroRepository.InsertCobroAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = cobro,
                Meta = {
                    Msj = cobro? "Success" : "No se ingreso el cobro",
                    ResponseId = requestId,
                    StatusCode = cobro? 200 : 404
                }
            };

            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateCobroAsync(CobroModificacion request, string requestId)
        {
            var cobro = await _cobroRepository.UpdateCobroAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = cobro,
                Meta = {
                    Msj = cobro? "Success" : "No se modifico la devolucion",
                    ResponseId = requestId,
                    StatusCode = cobro? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> DeshabilitarCobroAsync(CobroDeshabilitar request, string requestId)
        {
            var cobro = await _cobroRepository.DeshabilitarCobroAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = cobro,
                Meta = {
                    Msj = cobro? "Success" : "No se deshabilito el cobro",
                    ResponseId = requestId,
                    StatusCode = cobro? 200 : 404
                }
            };

            return response;
        }
    }
}
