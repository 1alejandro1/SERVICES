using BCP.CROSS.LOGGER;

namespace MICREDITO.WebSite.Helpers
{

    public class Log
    {
        public static void Debug(string format, params object[] objects)
        {
            Logger logger = new Logger();
            logger.Debug(format, objects);
        }
        public static void Debug(string message)
        {
            Logger logger = new Logger();
            logger.Debug(message);
        }
        public static void Error(string format, params object[] objects)
        {
            Logger logger = new Logger();
            logger.Error(format, objects);
        }
        public static void Error(string message)
        {
            Logger logger = new Logger();
            logger.Error(message);
        }
        public static void Fatal(string format, params object[] objects)
        {
            Logger logger = new Logger();
            logger.Fatal(format, objects);
        }
        public static void Fatal(string message)
        {
            Logger logger = new Logger();
            logger.Fatal(message);
        }
    }
}