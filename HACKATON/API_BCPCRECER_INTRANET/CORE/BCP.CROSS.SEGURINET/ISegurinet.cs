using BCP.CROSS.MODELS;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SEGURINET
{
    public interface ISegurinet
    {
        HealthCheckResult Check();
        Task<LoginResponse> Login(LoginRequest request);
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
    }
}
