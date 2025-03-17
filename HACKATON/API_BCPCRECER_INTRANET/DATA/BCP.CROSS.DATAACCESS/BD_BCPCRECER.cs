using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.DATAACCESS
{
    public class BD_BCPCRECER : DataAccessBase
    {
        public BD_BCPCRECER(IManagerSecrypt managerSecrypt, IOptions<DataBaseSettings> dataBaseSettings)
       : base(managerSecrypt, "BD_BCPCRECER", dataBaseSettings)
        {
        }
    }
}
