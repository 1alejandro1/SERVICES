using BCP.CROSS.SECRYPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.CROSS.DATAACCESS
{
    public class DataAccess : IDataAccess
    {
        private readonly DBOptions dBOption;
        private readonly IManagerSecrypt secrypt;

        public DataAccess(DBOptions dBOption, SecryptOptions secryptOptions)
        {
            this.dBOption = dBOption;
            this.secrypt = new ManagerSecrypt(secryptOptions.semilla);
        }
        private string Connection(DBOptionItems database)
        {
            string connection;
            try
            {
                string _password = this.secrypt.Desencriptar(database.password);
                connection = "Persist Security Info=True;User ID=" + database.user + ";Pwd=" + _password + ";Server=" + database.server + ";Database=" + database.dataBase + ";Application Name =" + dBOption.name;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }
        public async Task<Tuple<bool, string>> Check(string name)
        {
            Tuple<bool, string> _conexion = null;
            DBOptionItems item = this.dBOption.connections.FirstOrDefault(x => x.name == name);
            using var conexion = new SqlConnection(Connection(item));
            try
            {
                try
                {
                    await conexion.OpenAsync();
                    _conexion = new Tuple<bool, string>(true, $"BATABASE: {item.dataBase}; SERVER: {item.server}; USER: {item.user}");
                }
                catch (Exception ex)
                {
                    _conexion = new Tuple<bool, string>(false, $"COULD NOT CONNECT TO DATABASE: {item.dataBase} SERVER: {item.server}; USER: {item.user}; EXCEPTION: {ex.Message.ToUpper()}");
                }
                finally
                {
                    conexion.Close();
                    SqlConnection.ClearAllPools();
                }
            }
            catch (Exception ex)
            {
                _conexion = new Tuple<bool, string>(false, $"CONFIG PARMETER DATABASE: {name}; EXCEPTION: {ex.Message.ToUpper()}");
            }
            return _conexion;
        }
        public async Task<bool> ExecuteStoredProcedure(string name, string query, List<SqlParameter> parameter)
        {
            using SqlConnection conexion = new SqlConnection(Connection(this.dBOption.connections.FirstOrDefault(x => x.name == name)));
            try
            {
                SqlCommand comando = new SqlCommand(query, conexion)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 600000
                };
                foreach (var item in parameter)
                {
                    if (item.Value == null)
                        item.Value = DBNull.Value;
                    comando.Parameters.Add(item);
                }
                await conexion.OpenAsync();
                await comando.ExecuteNonQueryAsync();
                conexion.Close();
                SqlConnection.ClearAllPools();
                return true;
            }
            catch (SqlException Error)
            {
                conexion.Close();
                SqlConnection.ClearAllPools();
                throw Error;
            }
        }
        public async Task<DataTable> SelectStoredProcedure(string name, string query, List<SqlParameter> parameter)
        {
            DataTable Consulta = new DataTable();
            using SqlConnection conexion = new SqlConnection(Connection(this.dBOption.connections.FirstOrDefault(x => x.name == name)));
            try
            {
                SqlConnection.ClearAllPools();
                SqlDataAdapter comando = new SqlDataAdapter(query, conexion);
                comando.SelectCommand.CommandType = CommandType.StoredProcedure;
                comando.SelectCommand.CommandTimeout = 3600000;
                foreach (var item in parameter)
                {
                    if (item.Value == null)
                        item.Value = DBNull.Value;
                    comando.SelectCommand.Parameters.Add(item);
                }
                await conexion.OpenAsync();
                comando.Fill(Consulta);
            }
            catch (SqlException Error)
            {
                throw Error;
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearAllPools();
            }
            return Consulta;
        }
        public async Task<DataTable> Select(string name, string query)
        {
            DataTable Consulta = new DataTable();
            using SqlConnection conexion = new SqlConnection(Connection(this.dBOption.connections.FirstOrDefault(x => x.name == name)));
            try
            {
                SqlConnection.ClearAllPools();
                SqlDataAdapter comando = new SqlDataAdapter(query, conexion);
                comando.SelectCommand.CommandType = CommandType.Text;
                comando.SelectCommand.CommandTimeout = 3600000;
                await conexion.OpenAsync();
                comando.Fill(Consulta);
            }
            catch (SqlException Error)
            {
                throw Error;
            }
            finally
            {
                conexion.Close();
                SqlConnection.ClearAllPools();
            }
            return Consulta;
        }
    }
}
