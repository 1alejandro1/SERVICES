using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;

namespace BCP.CROSS.LOGGER
{
    public class Logger : ILogger
    {
        public const string header = "{0} Message: {1}";
        private readonly LoggerOptions _options;

        public Logger(LoggerOptions options)
        {
            this._options = options;
            LogEventLevel logEventLevel = LogEventLevel.Verbose;
            switch (options.LogEventLevel.ToUpper())
            {
                case "VERBOSE":
                    logEventLevel = LogEventLevel.Verbose;
                    break;
                case "DEBUG":
                    logEventLevel = LogEventLevel.Debug;
                    break;
                case "INFORMATION":
                    logEventLevel = LogEventLevel.Information;
                    break;
                case "WARNING":
                    logEventLevel = LogEventLevel.Warning;
                    break;
                case "ERROR":
                    logEventLevel = LogEventLevel.Error;
                    break;
                default:
                    break;
            }
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.File($"{this._options.Path}{DateTime.Now:ddMMyyyy}/{this._options.File}.txt",
                    logEventLevel,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: this._options.Template,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: (options.FileSizeLimitMegaBytes * 1000000),
                    rollOnFileSizeLimit: true)
                .MinimumLevel.Debug()
                .CreateLogger();
        }

        private static string GetStackTraceInfo()
        {
            var stackFrame = new StackTrace().GetFrame(2);
            string methodName = stackFrame.GetMethod().Name;
            string className = stackFrame.GetMethod().ReflectedType.FullName;
            return string.Format("Class: \"{0}\" Method: \"{1}\"", className, methodName);
        }

        public void Information(string message)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            Log.Information(string.Format(_header, location, message));
        }

        public void Debug(string message)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            Log.Debug(string.Format(_header, location, message));
        }

        public void Error(string exception)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            Log.Error(exception, string.Format(_header, location, ""));
        }

        public void Warning(string message)
        {
            string location = GetStackTraceInfo();
            string _header = header;
            Log.Warning(string.Format(_header, location, message));
        }
    }
}
