using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON
{
    public static class Constants
    {
        /// <summary>
        /// The error handling middleware error code.
        /// </summary>
        public static readonly int ErrorHandlingMiddlewareErrorCode = 500;

        /// <summary>
        /// The error middleware log.
        /// </summary>
        public static readonly string ErrorMiddlewareLog = "Ha ocurrido un error inesperado, revise los logs";
    }
}
