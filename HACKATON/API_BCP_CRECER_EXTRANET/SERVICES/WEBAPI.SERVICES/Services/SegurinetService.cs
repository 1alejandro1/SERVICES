using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
   public class SegurinetService: ISegurinetService
    {
        private ISegurinetRepository _segurinetRepository;
        public SegurinetService(ISegurinetRepository segurinetRepository)
        {
            this._segurinetRepository= segurinetRepository;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(ChangePasswordRequest request, string responseId)
        {
            var changePassword=await _segurinetRepository.ChangePasswordAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data= changePassword.state.Equals("00"),
                Meta =
                {
                    Msj=changePassword.message,
                    ResponseId=responseId,
                    StatusCode=changePassword.state.Equals("00")?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<LoginResponse>> GetLoginAsync(LoginRequest request, string responseId)
        {
            var login = await _segurinetRepository.GetLoginAsync(request);
            var response = new ServiceResponse<LoginResponse>
            {
                Data = login,
                Meta =
               {
                    Msj=login.Matricula.Substring(0,2).Equals("00")?"OPERACION EJECUTADA CORRECTAMENTE":login.Nombre,
                    ResponseId=responseId,
                    StatusCode=login.Matricula.Substring(0,2).Equals("00")?200:404
               }
            };
            response.Data.Matricula = response.Data.Matricula.Substring(2);
            return response;
        }
    }
}
