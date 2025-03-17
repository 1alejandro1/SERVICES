using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ISegurinetService
    {
        Task<ServiceResponse<LoginResponse>> GetLoginAsync(LoginRequest request, string responseId);
        Task<ServiceResponse<bool>> ChangePassword(ChangePasswordRequest request, string responseId);
    }
}
