using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IViasNotificationService
    {
        Task<ServiceResponse<List<ViasNotificacion>>> GetViasNotificacionAsync(string requestId);
    }
}
