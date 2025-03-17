using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Secat;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class SecatService : ISecatService
    {
        private readonly ISecatRepository _secatRepository;
        public SecatService(ISecatRepository secatRepository)
        {
            this._secatRepository = secatRepository;
        }

        public async Task<ServiceResponse<List<ATMResponse>>> GetATMAsync(string responseId)
        {
            var areas = await _secatRepository.GetATMAsync();
            var response = new ServiceResponse<List<ATMResponse>>()
            {
                Data = areas,
                Meta =
                {
                    Msj=areas!= null ? "Success" : "Ocurrio un error al cargar la lista de ATM",
                    ResponseId=responseId,
                    StatusCode=areas!= null?200:404
                }
            };
            return response;
        }
    }
}
