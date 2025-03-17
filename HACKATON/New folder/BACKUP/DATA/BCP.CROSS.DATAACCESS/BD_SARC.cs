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
    public class BD_SARC : DataAccessBase
    {
        public BD_SARC(IManagerSecrypt managerSecrypt, IOptions<DataBaseSettings> dataBaseSettings)
        : base(managerSecrypt, "BD_SARC", dataBaseSettings)
        {
        }
    }
}
