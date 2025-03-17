using BCP.CROSS.COMMON;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ISmtpService
    {
        Task<ServiceResponse<string>> EnviarCodigoCasoAsync(string codigoCaso, string emailCliente, string requestId);
    }
}
