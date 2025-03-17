using System.Collections.Generic;

namespace BCP.CROSS.DATAACCESS
{
    public class DBOptions
    {
        public List<DBOptionItems> connections { get; set; }
        public string name { get; set; }
    }

    public class DBOptionItems
    {
        public string name { get; set; }
        public string server { get; set; }
        public string dataBase { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}
