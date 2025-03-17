using BCP.CROSS.LOGGER;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BCP.Framework.Common.Middlewares
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
        private readonly ILogger logger;

        /// <summary>
        /// Indicates if the responses should contain exception details.
        /// </summary>
        private readonly bool includeExceptionDetailsInResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware" /> class.
        /// </summary>
        /// <param name="requestDelegate">Next middleware.</param>
        /// <param name="logger">Logger instance.</param>
        /// <param name="applicationSettings">Options accessor.</param>
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger logger, IOptions<ApplicationSettings> applicationSettings)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
            this.includeExceptionDetailsInResponse = applicationSettings.Value.IncludeExceptionStackInResponse;
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

                ServiceResponse<string> currentException = new()
                {
                    Data = null,
                    Message = Constants.ErrorMiddlewareLog,
                    Time = DateTime.UtcNow,
                    StatusCode = Constants.ErrorHandlingMiddlewareErrorCode,
                    Success = false
                };

                if (this.includeExceptionDetailsInResponse)
                {
                    currentException.Message = $"{innerException.Message}";
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(currentException)).ConfigureAwait(false);
            }
        }
    }
}

