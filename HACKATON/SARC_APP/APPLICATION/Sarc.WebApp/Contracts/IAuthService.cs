using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Login;
using System.Threading.Tasks;

namespace Sarc.WebApp.Contracts
{
    public interface IAuthService
    {
        Task<LoginResponse> GetToken();
        Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        Task<ServiceResponse<LoginResponse>> LoginAsyncTest(LoginRequest loginRequest);
    }
}