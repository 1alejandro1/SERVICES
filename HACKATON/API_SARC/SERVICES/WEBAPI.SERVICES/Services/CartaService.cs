using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Carta;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class CartaService: ICartaService
    {
        private readonly ICartaRepository _casoRepository;
        public CartaService(ICartaRepository casoRepository)
        {
            this._casoRepository = casoRepository;
        }
        public async Task<ServiceResponse<List<Carta>>> GetCartaAllAsync(string responseId)
        {
            var carta = await _casoRepository.GetCartaAllAsync();
            var response = new ServiceResponse<List<Carta>>()
            {
                Data = carta,
                Meta =
                {
                    Msj=carta!=null ? "Success" : "No se encontraron registros de cartas",
                    ResponseId=responseId,
                    StatusCode=carta!=null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<Carta>> GetCartaByIdAsync(CartaIdRequest request, string responseId)
        {
            var carta = await _casoRepository.GetCartaByIdAsync(request.CartaId);
            var response = new ServiceResponse<Carta>()
            {
                Data = carta,
                Meta =
                {
                    Msj=carta!=null ? "Success" : "No se encontraron registros de cartas",
                    ResponseId=responseId,
                    StatusCode=carta!=null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<string>> GetCartaFileAsync(CartaFileRequest request, string responseId)
        {
            var error = await _casoRepository.GetCartaFileAsync(request);
            var response = new ServiceResponse<string>()
            {
                Data = error,
                Meta =
                {
                    Msj=error!=null ? "Success" : "No se encontraron registros",
                    ResponseId=responseId,
                    StatusCode=error!=null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> InsertCartaAsync(Carta request, string responseId)
        {
            var carta = await _casoRepository.InsertCartaAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = carta,
                Meta =
                {
                    Msj=carta ? "Success" : "No se realizo el registro de la carta",
                    ResponseId=responseId,
                    StatusCode=carta?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateCartaAsync(Carta request, string responseId)
        {
            var carta = await _casoRepository.UpdateCartaAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = carta,
                Meta =
                {
                    Msj=carta ? "Success" : "No se realizo la modificacion de la carta",
                    ResponseId=responseId,
                    StatusCode=carta?200:404
                }
            };
            return response;
        }
    }
}
