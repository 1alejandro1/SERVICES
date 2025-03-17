using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Framework.Logs
{
    public interface ILoggerManager
    {
        void Information(string message);
        void Information(string format, params object[] objects);
        void Debug(string format, params object[] objects);
        void Debug(string message);
        void Error(string format, params object[] objects);
        void Error(string message);
        void Fatal(string format, params object[] objects);
        void Fatal(string message);
    }
}
