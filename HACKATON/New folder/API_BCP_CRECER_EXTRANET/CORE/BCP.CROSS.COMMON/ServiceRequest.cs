using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON
{
    public class ServiceRequest<T> : BaseRequest
    {
        public T Data { get; set; }
    }
}
