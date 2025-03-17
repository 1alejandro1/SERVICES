using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.DATAACCESS
{
    public class BD_REPEXT : DataAccessBase
    {
        public BD_REPEXT(IManagerSecrypt managerSecrypt, IOptions<DataBaseSettings> dataBaseSettings)
        : base(managerSecrypt, "RepExt", dataBaseSettings)
        {
        }
    }
}
