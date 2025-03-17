namespace BCP.CROSS.LOGGER
{
    public interface ILogger
    {
        void Information(string message);
        void Debug(string message);
        void Warning(string message);
        void Error(string exception);
    }
}
