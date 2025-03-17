using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Crecer;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;
using BCP.CROSS.MODELS.DTOs.Crecer;

namespace WEBAPI.SERVICES.Services
{
    public class CrecerService : ICrecerService
    {
        private readonly ICrecerRepository _crecerRepository;
        public CrecerService(ICrecerRepository crecerRepository)
        {
            this._crecerRepository = crecerRepository;
        }

        public async Task<ServiceResponse<List<CategoriasResponse>>> GetCategoriasAsync(string responseId)
        {
            var areas = await _crecerRepository.GetCategoriasAsync();
            var response = new ServiceResponse<List<CategoriasResponse>>()
            {
                Data = areas,
                Meta =
                {
                    Msj=areas!= null ? "Success" : "Ocurrio un error al cargar la lista de Categorías",
                    ResponseId=responseId,
                    StatusCode=areas!= null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<List<ObtieneEmpresasByCategoriaCiudadResponse>>> ObtieneEmpresasByCategoriaCiudadAsync(ObtieneEmpresasByCategoriaCiudadRequest CuentasRequest, string requestId)
        {

            var responseCobros = await _crecerRepository.ObtieneEmpresasByCategoriaCiudadAsync(CuentasRequest);
            var response = new ServiceResponse<List<ObtieneEmpresasByCategoriaCiudadResponse>>()
            {
                Data = responseCobros,
                Meta = {
                    Msj = responseCobros != null ? "Success" : "Cobros no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCobros != null ? 200 : 404
                }
            };

            return response;
        }
    }
}
