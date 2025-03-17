using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.DATAACCESS
{
    public interface IDataAccessBase
    {
        HealthCheckResult Check();
        Task<bool> ExecuteSP(string storeProcedure, List<SqlParameter> parameters);
        Task<bool> ExecuteSP_bool(string storeProcedure, List<SqlParameter> parameters);
        Task<int> ExecuteSP_int(string storeProcedure, List<SqlParameter> parameters);
        Task<DataTable> ExecuteSP_DataTable(string storeProcedure, List<SqlParameter> parameters);
    }
}
