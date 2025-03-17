using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Secat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface ISecatService
    {
        Task<ServiceResponse<List<ATMResponse>>> GetATMAsync(string responseId);
    }
}
