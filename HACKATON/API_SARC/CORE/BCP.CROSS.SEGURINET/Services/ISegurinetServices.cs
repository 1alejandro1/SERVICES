using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SEGURINET.Services
{
    public interface ISegurinetServices
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
    }
}
