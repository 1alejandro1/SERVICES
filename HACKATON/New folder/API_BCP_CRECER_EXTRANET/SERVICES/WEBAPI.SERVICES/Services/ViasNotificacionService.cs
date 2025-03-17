using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES
{
    public class ViasNotificacionService: IViasNotificationService
    {
        private readonly IViasNotificacionRepository _viasNotificacionRepository;

        public ViasNotificacionService(IViasNotificacionRepository viasNotificacionRepository)
        {
            _viasNotificacionRepository = viasNotificacionRepository;
        }

        public async Task<ServiceResponse<List<ViasNotificacion>>> GetViasNotificacionAsync(string requestId)
        {
            var vias = await _viasNotificacionRepository.GetViasNotificacionAsync();
            var response = new ServiceResponse<List<ViasNotificacion>>
            {
                Data = vias,
                Meta = {
                    Msj = vias != null ? "Success" : "No existen registros",
                    ResponseId = requestId,
                    StatusCode = vias != null ? 200 : 404
                }
            };

            return response;
        }
    }
}
