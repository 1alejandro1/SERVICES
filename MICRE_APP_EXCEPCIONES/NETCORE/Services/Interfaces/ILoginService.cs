using NETCORE.Models;

namespace NETCORE.Services.Interfaces
{
    public interface ILoginService
    {
        Task<TokenResponse> GetToken(User user);
    }
}
