using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ISegurinetRepository
    {
        Task<LoginResponse> GetLoginAsync(LoginRequest request);
        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request);
    }
}
