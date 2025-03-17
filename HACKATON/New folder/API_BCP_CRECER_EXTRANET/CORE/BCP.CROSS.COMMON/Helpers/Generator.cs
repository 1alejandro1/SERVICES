using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON.Helpers
{
    public class Generator
    {
        public static string RequestId(string requestId) =>
            string.IsNullOrEmpty(requestId) ? DateTime.Now.ToString("yyyyMMddhhmmssff") : requestId;
    }
}
