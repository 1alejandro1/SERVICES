using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Carta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ICartaService
    {
        Task<ServiceResponse<List<Carta>>> GetCartaAllAsync(string responseId);
        Task<ServiceResponse<Carta>> GetCartaByIdAsync(CartaIdRequest request, string responseId);
        Task<ServiceResponse<bool>> InsertCartaAsync(Carta request, string responseId);
        Task<ServiceResponse<bool>> UpdateCartaAsync(Carta request, string responseId);
        Task<ServiceResponse<string>> GetCartaFileAsync(CartaFileRequest request, string responseId);
    }
}
