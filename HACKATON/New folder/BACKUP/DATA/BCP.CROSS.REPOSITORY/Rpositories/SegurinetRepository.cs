using BCP.CROSS.MODELS;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.SEGURINET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class SegurinetRepository : ISegurinetRepository
    {
        private readonly Segurinet _segurinet;
        public SegurinetRepository(Segurinet segurinet)
        {
            this._segurinet = segurinet;
        }

        public async  Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var result = await this._segurinet.ChangePassword(request);
            return result;
        }

        public async Task<LoginResponse> GetLoginAsync(LoginRequest request)
        {
            var result = await this._segurinet.Login(request);
            return result;
        }
    }
}
