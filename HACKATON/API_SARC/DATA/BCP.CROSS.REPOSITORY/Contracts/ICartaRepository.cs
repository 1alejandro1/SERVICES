using BCP.CROSS.MODELS.Carta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ICartaRepository
    {
        Task<List<Carta>> GetCartaAllAsync();
        Task<Carta> GetCartaByIdAsync(string cartaId);
        Task<bool> InsertCartaAsync(Carta request);
        Task<bool> UpdateCartaAsync(Carta request);
        Task<string> GetCartaFileAsync(CartaFileRequest request);
    }
}
