using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Secat;
using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface ISecatRepository
    {
        Task<List<ATMResponse>> GetATMAsync();
    }
}
