/// <summary>
/// Libreria para realizar la escritura de logs.
/// </summary>
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;
using System.Threading;
/// <summary>
/// Capa de Framework para la escritura de logs
/// </summary>
namespace BCP.Framework.Logs
{
    /// <summary>
    /// Clase de los Logs
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        /// <summary>
        /// Ubicacion Fisica donde se escribira los logs
        /// </summary>
        private static string _pathLogFile;
        /// <summary>
        /// Cabecera de los logs 
        /// </summary>
        public const string header = "INFO: {0} DETALLE: {1}";
        private readonly LogsSettings _logsSettings;

        /// <summary>
        /// Constructor para definir la ubicacion fisica y nivel de logs que se escribiran en el archivo.
        /// </summary>
        /// <param name="pathLogFile"></param>
        /// <param name="level"></param>
        public LoggerManager(IOptions<LogsSettings> logsSettings)
        {
            _logsSettings = logsSettings.Value;
            /// <summary>
            /// Clasificacion de los LOGS 
            /// </summary>
            switch (_logsSettings.Level)
            {
                case "INFORMATION":
                    /// <summary>
                    /// Configuracion para la escritura de logs, ubicacion fisica, hilo dedicado para la escritura del mismo, uso de un template para creacion de logs, y elformato del nombre para el guardado por dia.
                    /// </summary>
                    _pathLogFile = _logsSettings.PathFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Information()
                        .CreateLogger();
                    break;
                case "FATAL":
                    _pathLogFile = _logsSettings.PathFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Fatal()
                        .CreateLogger();
                    break;
                case "WARNING":
                    _pathLogFile = _logsSettings.PathFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Warning()
                        .CreateLogger();
                    break;
                case "ERROR":
                    _pathLogFile = _logsSettings.PathFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Error()
                        .CreateLogger();
                    break;
                case "DEBUG":
                    _pathLogFile = _logsSettings.PathFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Debug()
                        .CreateLogger();
                    break;
                default:
                    _pathLogFile = _logsSettings.PathFile;
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(_pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}")
                        .MinimumLevel.Verbose()
                        .CreateLogger();
                    break;
            }
        }
        /// <summary>
        /// Administracion del hilo de escritura del Log
        /// </summary>
        class ThreadIdEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                        "ThreadId", Thread.CurrentThread.ManagedThreadId));
            }
        }
        /// <summary>
        /// Metodo de obtencion de la clase y funcion donde se pidio la escritura del log.
        /// </summary>
        /// <returns></returns>
        private static string GetStackTraceInfo()
        {
            var stackFrame = new StackTrace().GetFrame(2);
            string methodName = stackFrame.GetMethod().Name;
            string className = stackFrame.GetMethod().ReflectedType.FullName;
            return string.Format("Class: \"{0}\" Method: \"{1}\"", className, methodName);
        }
        /// <summary>
        /// Funciones para la escritura de logs, segun el texto enviado
        /// </summary>
        /// <param name="format">Es el texto plano {0}</param>
        /// <param name="objects">el objeto para completar el texto plano</param>
        public void Information(string format, params object[] objects)
        {
            string location = GetStackTraceInfo();
            string message = string.Format(format, objects);
            Log.Information(string.Format(header, location, message));
        }
        public void Information(string message)
        {
            string location = GetStackTraceInfo();
            Log.Information(string.Format(header, location, message));
        }

        public void Debug(string format, params object[] objects)
        {
            string location = GetStackTraceInfo();
            string message = string.Format(format, objects);
            Log.Debug(string.Format(header, location, message));
        }
        public void Debug(string message)
        {
            string location = GetStackTraceInfo();
            Log.Debug(string.Format(header, location, message));
        }

        public void Error(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            Log.Error(string.Format(header, location, message));
        }

        public void Error(string message)
        {
            string location = GetStackTraceInfo();
            Log.Error(string.Format(header, location, message));
        }

        public void Fatal(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            Log.Fatal(string.Format(header, location, message));
        }

        public void Fatal(string message)
        {
            string location = GetStackTraceInfo();
            Log.Fatal(string.Format(header, location, message));
        }
    }
}

