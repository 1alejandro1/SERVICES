using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Login;
using BCP.CROSS.MODELS.Segurinet;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface IAuthService
    {
        Task<ApiSegurinetResponse> GetToken();
        Task<ApiSegurinetResponse> LoginAsync(ApiSegurinetRequest loginRequest);
        Task<ServiceResponse<LoginResponse>> LoginAsyncTest(LoginRequest loginRequest);
    }
}
