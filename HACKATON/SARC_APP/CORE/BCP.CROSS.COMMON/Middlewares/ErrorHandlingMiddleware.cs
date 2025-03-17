using System;
using System.Threading.Tasks;
using BCP.Framework.Logs;
using Microsoft.AspNetCore.Http;

namespace BCP.CROSS.COMMON.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Reference to the next middleware.
        /// </summary>
        private readonly RequestDelegate requestDelegate;

        /// <summary>
        /// Instance of the logger.
        /// </summary>
        private readonly ILoggerManager logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware" /> class.
        /// </summary>
        /// <param name="requestDelegate">Next middleware.</param>
        /// <param name="logger">Logger instance.</param>
        /// <param name="applicationSettings">Options accessor.</param>
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILoggerManager logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        /// <summary>
        /// Method is invoked when middleware is switched.
        /// </summary>
        /// <param name="context">Context of the invocation.</param>
        /// <returns>Task of the invocation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (this.requestDelegate != null)
                {
                    // invoking next middleware.
                    await this.requestDelegate.Invoke(context).ConfigureAwait(false);
                }
            }
            catch (Exception innerException)
            {
                this.logger.Error($"{System.Diagnostics.Activity.Current?.RootId}: {innerException.Message} - {innerException.StackTrace}");
                throw innerException;
            }
        }
    }
}

