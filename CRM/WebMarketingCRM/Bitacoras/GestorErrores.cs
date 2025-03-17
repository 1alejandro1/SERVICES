using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using log4net.Core;
using System.Web;
using System.Security.Principal;
using System.Reflection;

namespace Bitacoras
{
    public class GestorErrores
    {
        
    }

    public class CLogger : log4net.Core.LogImpl, IBitacora
    {
        #region Members
        //private static readonly ILog logger = LogManager.GetLogger(typeof(CLogger));
        private readonly static Type EsteTipo = typeof(CLogger);

        private readonly static Level DEBUG = new Level(30000, "DEBUG");
        private readonly static Level INFORMACION = new Level(40000, "INFORMACION");
        private readonly static Level ADVERTENCIA = new Level(60000, "ADVERTENCIA");
        private readonly static Level ERROR = new Level(70000, "ERROR");

        #endregion

        #region Constructors
        static CLogger()
        {
            XmlConfigurator.Configure();
        }
        public CLogger(ILogger logger)
            : base(logger)
        { }
        #endregion

        #region Methods
        public void Debug(string proceso, string log)
        {
            Debug(proceso, log, null);
        }
        public void Debug(string proceso, string log, Exception ex)
        {
            LoggingEvent loggingEvent = new LoggingEvent(EsteTipo, Logger.Repository, Logger.Name, DEBUG, log, ex);
            loggingEvent.Properties["Modulo"] = proceso;
            loggingEvent.Properties["Usuario"] = HttpContext.Current != null
                                     ? HttpContext.Current.User.Identity.Name
                                     : (WindowsIdentity.GetCurrent() != null
                                            ? System.Security.Principal.WindowsIdentity.GetCurrent().Name
                                            : "No Identificado.");
            Logger.Log(loggingEvent);
        }
        public void Informacion(string proceso, string log)
        {
            Informacion(proceso, log, null);
        }
        public void Informacion(string proceso, string log, Exception ex)
        {
            LoggingEvent loggingEvent = new LoggingEvent(EsteTipo, Logger.Repository, Logger.Name, INFORMACION, log, ex);
            loggingEvent.Properties["Modulo"] = proceso;
            loggingEvent.Properties["Usuario"] = HttpContext.Current != null
                                     ? HttpContext.Current.User.Identity.Name
                                     : (WindowsIdentity.GetCurrent() != null
                                            ? System.Security.Principal.WindowsIdentity.GetCurrent().Name
                                            : "No Identificado.");
            Logger.Log(loggingEvent);
        }
        public void Advertencia(string proceso, string log)
        {
            Advertencia(proceso, log, null);
        }
        public void Advertencia(string proceso, string log, Exception ex)
        {
            LoggingEvent loggingEvent = new LoggingEvent(EsteTipo, Logger.Repository, Logger.Name, ADVERTENCIA, log, ex);
            loggingEvent.Properties["Modulo"] = proceso;
            loggingEvent.Properties["Usuario"] = HttpContext.Current != null
                                     ? HttpContext.Current.User.Identity.Name
                                     : (WindowsIdentity.GetCurrent() != null
                                            ? System.Security.Principal.WindowsIdentity.GetCurrent().Name
                                            : "No Identificado.");
            Logger.Log(loggingEvent);
        }
        public void Error(string proceso, string log)
        {
            Error(proceso, log, null);
        }
        public void Error(string proceso, string log, Exception ex)
        {
            LoggingEvent loggingEvent = new LoggingEvent(EsteTipo, Logger.Repository, Logger.Name, ERROR, log, ex);
            loggingEvent.Properties["Modulo"] = proceso;
            loggingEvent.Properties["Usuario"] = HttpContext.Current != null
                                     ? HttpContext.Current.User.Identity.Name
                                     : (WindowsIdentity.GetCurrent() != null
                                            ? System.Security.Principal.WindowsIdentity.GetCurrent().Name
                                            : "No Identificado.");
            Logger.Log(loggingEvent);
        }
        #endregion
    }
    public class BitacoraAdmin
    {
        private BitacoraAdmin() { }
        private static readonly WrapperMap s_wrapperMap = new WrapperMap(WrapperCreationHandler);
        private static IBitacora WrapLogger(ILogger logger)
        {
            return (IBitacora)s_wrapperMap.GetWrapper(logger);
        }
        private static IBitacora[] WrapLoggers(ILogger[] loggers)
        {
            IBitacora[] results = new IBitacora[loggers.Length];
            for (int i = 0; i < loggers.Length; i++)
            {
                results[i] = WrapLogger(loggers[i]);
            }
            return results;
        }
        private static ILoggerWrapper WrapperCreationHandler(ILogger logger)
        {
            return new CLogger(logger);
        }
        public static IBitacora Existe(string nombre)
        {
            return Existe(Assembly.GetCallingAssembly(), nombre);
        }
        public static IBitacora Existe(string dominio, string nombre)
        {
            return WrapLogger(LoggerManager.Exists(dominio, nombre));
        }
        public static IBitacora Existe(Assembly assembly, string nombre)
        {
            return WrapLogger(LoggerManager.Exists(assembly, nombre));
        }
        public static IBitacora[] ObtenerLoggersActuales()
        {
            return ObtenerLoggersActuales(Assembly.GetCallingAssembly());
        }
        public static IBitacora[] ObtenerLoggersActuales(string dominio)
        {
            return WrapLoggers(LoggerManager.GetCurrentLoggers(dominio));
        }
        public static IBitacora[] ObtenerLoggersActuales(Assembly assembly)
        {
            return WrapLoggers(LoggerManager.GetCurrentLoggers(assembly));
        }
        public static IBitacora ObtenerLogger(object nombre)
        {
            return ObtenerLogger(Assembly.GetCallingAssembly(), nombre.ToString());
        }
        public static IBitacora ObtenerLogger(string nombre)
        {
            return ObtenerLogger(Assembly.GetCallingAssembly(), nombre);
        }
        public static IBitacora ObtenerLogger(string dominio, string nombre)
        {
            return WrapLogger(LoggerManager.GetLogger(dominio, nombre));
        }
        public static IBitacora ObtenerLogger(Assembly assembly, string nombre)
        {
            return WrapLogger(LoggerManager.GetLogger(assembly, nombre));
        }
        public static IBitacora ObtenerLogger(Type type)
        {
            return ObtenerLogger(Assembly.GetCallingAssembly(), type.FullName);
        }
        public static IBitacora ObtenerLogger(string dominio, Type type)
        {
            return WrapLogger(LoggerManager.GetLogger(dominio, type));
        }
        public static IBitacora ObtenerLogger(Assembly assembly, Type type)
        {
            return WrapLogger(LoggerManager.GetLogger(assembly, type));
        }
    }
    public interface IBitacora
    {
        void Debug(string proceso, string log);
        void Debug(string proceso, string log, Exception ex);
        void Informacion(string proceso, string log);
        void Informacion(string proceso, string log, Exception ex);
        void Advertencia(string proceso, string log);
        void Advertencia(string proceso, string log, Exception ex);
        void Error(string proceso, string log);
        void Error(string proceso, string log, Exception ex);
    }
}
