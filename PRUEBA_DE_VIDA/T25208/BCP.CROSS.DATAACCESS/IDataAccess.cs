using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BCP.CROSS.DATAACCESS
{
    public interface IDataAccess
    {
        Task<Tuple<bool, string>> Check(string name);
        Task<bool> ExecuteStoredProcedure(string name, string query, List<SqlParameter> parameter);
        Task<DataTable> SelectStoredProcedure(string name, string query, List<SqlParameter> parameter);
        Task<DataTable> Select(string name, string query);
    }
}
