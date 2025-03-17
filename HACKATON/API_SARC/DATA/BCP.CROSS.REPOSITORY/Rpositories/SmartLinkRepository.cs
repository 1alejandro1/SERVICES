using BCP.CROSS.MODELS;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.SMARTLINK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class SmartLinkRepository :ISmartLinkRepository
    {
        private readonly SmartLink _srvSmartLink;
        public SmartLinkRepository(SmartLink srvSmartLink)
        {
            this._srvSmartLink = srvSmartLink;
        }

        public async Task<TipoCambio> GetTipoCambioAsync()
        {
            return await this._srvSmartLink.GetTipoCambioAsync();
        }
    }
}
