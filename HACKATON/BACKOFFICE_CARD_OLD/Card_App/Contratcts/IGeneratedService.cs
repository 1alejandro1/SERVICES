using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Generated;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface IGeneratedService
    {
        Task<GeneratedResponse> Generated(GeneratedRequest getClientByIdcRequest);
    }
}
