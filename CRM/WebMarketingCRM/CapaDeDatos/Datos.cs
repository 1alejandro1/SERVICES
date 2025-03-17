using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitacoras;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaDeDatos
{
    public class Datos
    {
        #region Parametros de Bitacoras
        //INICIALIZA BITACORAS POR PARAMETROS
        public enum TiposLogger
        { EV, TextLog }
        private static readonly IBitacora Bitacora = BitacoraAdmin.ObtenerLogger("BitacoraEventLog");
        private static readonly IBitacora BitacoraText = BitacoraAdmin.ObtenerLogger(TiposLogger.TextLog);
        private static readonly string Destinatario = ConfigurationManager.AppSettings.Get("Destinatario");
        private static readonly string Asunto = ConfigurationManager.AppSettings.Get("Asunto");

        #endregion
        #region Variables Locales
        Conexion con = new Conexion();
        #endregion

        public DataSet CampaniasMarketing()
        {
            DataSet ds = new DataSet();
            try
            {
                string strCadenaConexion = con.strConexionBD();
                BitacoraText.Informacion("Conexion", strCadenaConexion);
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.SP_CampaniasMarketing", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDa.Fill(ds, "Marketing");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("CampaniasMarketing()", "Error Controlado ", ex);
                BitacoraText.Error("CampaniasMarketing()", "Error Controlado", ex);
                ds = null;
            }
            return ds;
        }
        public DataSet ControlEjecucion(string Accion)
        {
            DataSet ds = new DataSet();
            try
            {
                string strCadenaConexion = con.strConexionBD();
                BitacoraText.Informacion("Conexion", strCadenaConexion);
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.ActividadesModificar", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Accion", SqlDbType.NVarChar, 30).Value = Accion;
                        sqlCmd.CommandTimeout = 0;
                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDa.Fill(ds, "ControlEjecucion");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("ControlEjecucion()", "Error Controlado ", ex);
                BitacoraText.Error("ControlEjecucion()", "Error Controlado", ex);
                ds = null;
            }
            return ds;
        }
        public DataSet LogMarketing(string CodigoCampania)
        {
            DataSet ds = new DataSet();
            try
            {
                string strCadenaConexion = con.strConexionBD();

                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.SP_LogMarketing", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@CodCampania", SqlDbType.NVarChar, 30).Value = CodigoCampania;
                        sqlCmd.CommandTimeout = 0;
                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDa.Fill(ds, "LogMarketing");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("LogMarketing()", "Error Controlado ", ex);
                BitacoraText.Error("LogMarketing()", "Error Controlado", ex);
                ds = null;
            }
            return ds;
        }

        public DataSet BuenDiaImagen(Guid Id)
        {
            DataSet ds = new DataSet();
          
            try
            {
                string strCadenaConexion = con.strConexionBD();

                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.CRM_NombreImagen_byID", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Id;
                        sqlCmd.CommandTimeout = 0;

                        
                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDa.Fill(ds, "BuenDia");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("BuenDiaImagen()", "Error Controlado ", ex);
                BitacoraText.Error("BuenDiaImagen()", "Error Controlado", ex);
                ds = null;
            }
            return ds;
        }

        public DataSet DatosCliente(Guid Id)
        {
            DataSet ds = new DataSet();

            try
            {
                string strCadenaConexion = con.strConexionBD();

                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.CRM_NombreCIC_byID", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Id;
                        sqlCmd.CommandTimeout = 0;


                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDa.Fill(ds, "DatoCliente");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("DatosCliente()", "Error Controlado ", ex);
                BitacoraText.Error("DatosCliente()", "Error Controlado", ex);
                ds = null;
            }
            return ds;
        }
        public DataSet LogMarketingCarga(string CodigoCampania)
        {
            DataSet ds = new DataSet();
            try
            {
                string strCadenaConexion = con.strConexionBD();

                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.SP_logErrorCargaMarketing", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@CodCampania", SqlDbType.NVarChar, 30).Value = CodigoCampania;
                        sqlCmd.CommandTimeout = 0;
                        using (SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd))
                        {
                            sqlDa.Fill(ds, "LogMarketingError");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("LogMarketingCarga()", "Error Controlado ", ex);
                BitacoraText.Error("LogMarketingCarga()", "Error Controlado", ex);
                ds = null;
            }
            return ds;
        }
        public DataTable ObtenerCabecera(string codigoCampania)
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("SP_DatosCampaniaMarketing", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@CodCampania", SqlDbType.NVarChar, 30).Value = codigoCampania;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("ObtenerCabecera(" + codigoCampania + ")", "Error Controlado ", ex);
                BitacoraText.Error("ObtenerCabecera(" + codigoCampania + ")", "Error Controlado", ex);
                return null;
            }


        }
        public int RegistraGestion(string IdEntidad, int Respuesta, double Monto, int Probabilidad, string Fecha,string Asignado, string DomainUser, string IdPadre, string Descripcion)
        {
            try
            {
                int resp = 0;
                string strCadenaConexion = con.strConexionBD();
                
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("CRM_RegistraDetalleAgenda", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@IdEntidad", SqlDbType.NVarChar, 100).Value = IdEntidad;
                        sqlCmd.Parameters.Add("@Respuesta", SqlDbType.Int).Value = Respuesta;
                        sqlCmd.Parameters.Add("@Monto", SqlDbType.Float).Value = Monto;
                        sqlCmd.Parameters.Add("@Probabilidad", SqlDbType.Float).Value = Probabilidad;
                        sqlCmd.Parameters.Add("@Fecha", SqlDbType.NVarChar, 10).Value = Fecha;
                        sqlCmd.Parameters.Add("@Asignado", SqlDbType.NVarChar, 20).Value = Asignado;
                        sqlCmd.Parameters.Add("@DomainName", SqlDbType.NVarChar, 100).Value = DomainUser;
                        sqlCmd.Parameters.Add("@IdPadre", SqlDbType.NVarChar, 100).Value = IdPadre;
                        sqlCmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 500).Value = Descripcion;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.ExecuteNonQuery();
                        
                    }
                }
                return resp;

            }
            catch (Exception ex)
            {
                Bitacora.Error("RegistraGestion", "Error Controlado ", ex);
                BitacoraText.Error("RegistraGestion", "Error Controlado", ex);
                return -1;
            }


        }
        public DataTable ObtenerAgenda(string IdEntidad, string DomainName)
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("CRM_SeleccionaDetalleAgendaByID", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@EntidadID", SqlDbType.UniqueIdentifier).Value = new Guid(IdEntidad);
                        sqlCmd.Parameters.Add("@DomainName", SqlDbType.NChar, 100).Value = DomainName;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("ObtenerAgenda(" + IdEntidad + ")", "Error Controlado ", ex);
                BitacoraText.Error("ObtenerAgenda(" + IdEntidad + ")", "Error Controlado", ex);
                return null;
            }


        }
        public DataTable ObtenerDetalle(string codigoCampania)
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("SP_DatosCampaniaMarketingMiembro", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@CodCampania", SqlDbType.NVarChar, 30).Value = codigoCampania;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("ObtenerCabecera(" + codigoCampania + ")", "Error Controlado ", ex);
                BitacoraText.Error("ObtenerCabecera(" + codigoCampania + ")", "Error Controlado", ex);
                return null;
            }

        }
        public DataTable ActualizacionPropietarios()
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("SP_DatosClientePropietario", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("ActualizacionPropietarios()", "Error Controlado ", ex);
                BitacoraText.Error("ActualizacionPropietarios()", "Error Controlado", ex);
                return null;
            }

        }
        public DataTable ActualizacionDatosCliente()
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("SP_ActualizacionDatosCliente", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("ActualizacionDatosCliente()", "Error Controlado ", ex);
                BitacoraText.Error("ActualizacionDatosCliente()", "Error Controlado", ex);
                return null;
            }

        }
        public DataTable CierreActividadesMarketing()
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("SP_CierreActividadesMarketing", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("CierreActividadesMarketing()", "Error Controlado ", ex);
                BitacoraText.Error("CierreActividadesMarketing()", "Error Controlado", ex);
                return null;
            }

        }
        public DataTable DatosLogEjecucion(int tipo, string codigoCampania)
        {
            try
            {
                string strCadenaConexion = con.strConexionBD();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa;
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("SP_DatosLogEjecucion", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = tipo;
                        sqlCmd.Parameters.Add("@CodCampania", SqlDbType.NVarChar, 30).Value = codigoCampania;
                        sqlCmd.CommandTimeout = 0;
                        sqlDa = new SqlDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                Bitacora.Error("DatosLogEjecucion(" + tipo.ToString() + ", " + codigoCampania + ")", "Error Controlado ", ex);
                BitacoraText.Error("DatosLogEjecucion(" + tipo.ToString() + ", " + codigoCampania + ")", "Error Controlado", ex);
                return null;
            }

        }
        public string EjecucionProceso(string codigoCampania)
        {
            string mensaje = "Proceso ejecutado con exito";
            try
            {
                string strCadenaConexion = con.strConexionBD();
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("RegistraProductosPreaprobadosMod", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@codigoCampania", SqlDbType.NVarChar, 30).Value = codigoCampania;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.ExecuteScalar();
                    }
                }
                return mensaje;

            }
            catch (Exception ex)
            {
                Bitacora.Error("EjecucionProceso(" + codigoCampania + ")", "Error Controlado ", ex);
                BitacoraText.Error("EjecucionProceso(" + codigoCampania + ")", "Error Controlado", ex);
                mensaje = ex.Message;
                return mensaje;
            }

        }
        public string EjecucionEliminacion(string Proceso, string Accion, string Fecha)
        {
            string mensaje = "Proceso ejecutado con exito";
            try
            {
                string strCadenaConexion = con.strConexionBD();
                using (SqlConnection sqlConexion = new SqlConnection(strCadenaConexion))
                {
                    sqlConexion.Open();
                    using (SqlCommand sqlCmd = new SqlCommand("RegistraControlEjecucion", sqlConexion))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@Proceso", SqlDbType.NVarChar, 30).Value = Proceso;
                        sqlCmd.Parameters.Add("@Accion", SqlDbType.NVarChar, 20).Value = Accion;
                        sqlCmd.Parameters.Add("@Fecha", SqlDbType.NVarChar, 10).Value = Fecha;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.ExecuteScalar();
                    }
                }
                return mensaje;

            }
            catch (Exception ex)
            {
                Bitacora.Error("EjecucionEliminacion(" + Proceso + ")", "Error Controlado ", ex);
                BitacoraText.Error("EjecucionEliminacion(" + Proceso + ")", "Error Controlado", ex);
                mensaje = ex.Message;
                return mensaje;
            }

        }
    }
}
