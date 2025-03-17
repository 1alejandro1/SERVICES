using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Enroll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Card_App.Contratcts
{
    public interface IEnrollService
    {
        Task<ServiceResponse<bool?>> Enroll(EnrollRequest Request);
    }
}
