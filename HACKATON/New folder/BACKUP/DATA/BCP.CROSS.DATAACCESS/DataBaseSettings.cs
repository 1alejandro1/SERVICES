using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.DATAACCESS
{
    public class DataBaseSettings
    {
        public List<ConnectionString> ConnectionStrings { get; set; }
    }
    public class ConnectionString
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
        
    }
}
