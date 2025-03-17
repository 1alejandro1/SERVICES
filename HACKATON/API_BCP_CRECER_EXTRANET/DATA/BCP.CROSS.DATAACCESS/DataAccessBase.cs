using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
    public class DataAccessBase : IDataAccessBase
    {
        private readonly IManagerSecrypt _managerSecrypt;
        private readonly ConnectionString _connectionDb;

        public DataAccessBase(IManagerSecrypt managerSecrypt, string nameBd, IOptions<DataBaseSettings> dataBaseSettings)
        {
            _connectionDb = dataBaseSettings.Value.ConnectionStrings.FirstOrDefault(x => x.Name.Equals(nameBd));
        }

        public HealthCheckResult Check()
        {
            try
            {
                try
                {
                    var conexion = new SqlConnection(Connection());
                    conexion.Open();
                    conexion.Close();
                    var response = HealthCheckResult.Healthy($"Data Base: {_connectionDb.DataBase}; Server: {_connectionDb.Server}; User: {_connectionDb.User}");
                    return response;
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Could not connecto to Data Base: {_connectionDb.DataBase} Server: {_connectionDb.Server}; User: {_connectionDb.User}; Exception: {ex.Message.ToUpper()}");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Config parameter Data Base: {_connectionDb.DataBase}; Exception: {ex.Message.ToUpper()}");
            }
        }

        public async Task<bool> ExecuteSP(string storeProcedure, List<SqlParameter> parameters = default)
        {
            SqlConnection sqlConnection = new(Connection());
            SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = _connectionDb.Timeout;

            if (parameters is not null)
            {
                foreach (var item in parameters)
                {
                    item.Value ??= DBNull.Value;
                    sqlCommand.Parameters.Add(item);
                }
            }

            try
            {
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                sqlConnection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }
        public async Task<bool> ExecuteSP_bool(string storeProcedure, List<SqlParameter> parameters = default)
        {
            SqlConnection sqlConnection = new(Connection());
            SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = _connectionDb.Timeout;

            if (parameters is not null)
            {
                foreach (var item in parameters)
                {
                    item.Value ??= DBNull.Value;
                    sqlCommand.Parameters.Add(item);
                }
            }

            try
            {
                await sqlConnection.OpenAsync();
                bool success = Convert.ToBoolean(await sqlCommand.ExecuteScalarAsync());
                sqlConnection.Close();
                return success;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task<int> ExecuteSP_int(string storeProcedure, List<SqlParameter> parameters = default)
        {
            SqlConnection sqlConnection = new(Connection());
            SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = _connectionDb.Timeout;

            if (parameters is not null)
            {
                foreach (var item in parameters)
                {
                    item.Value ??= DBNull.Value;
                    sqlCommand.Parameters.Add(item);
                }
            }

            try
            {
                return Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task<DataTable> ExecuteSP_DataTable(string storeProcedure, List<SqlParameter> parameters = default)
        {
            DataTable dataTable = new();
            var cn = Connection();
            SqlConnection sqlConnection = new(cn);
            SqlCommand sqlCommand = new(storeProcedure, sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = _connectionDb.Timeout;

            if (parameters is not null)
            {
                foreach (var item in parameters)
                {
                    item.Value ??= DBNull.Value;
                    sqlCommand.Parameters.Add(item);
                }
            }

            try
            {
                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();
                dataTable.Load(reader);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
            return dataTable;
        }

        private string Connection()
        {
            string connection = string.Empty;
            try
            {
                string pass = _connectionDb.Password;
                connection = "Persist Security Info=True;User ID=" + _connectionDb.User + ";Password=" + pass + ";Server=" + _connectionDb.Server + ";Database=" + _connectionDb.DataBase;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }
    }
}
