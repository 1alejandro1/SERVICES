using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos;
using System.Data;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Bitacoras;
using System.Configuration;

namespace CapaDeNegocios
{
    public class Negocios
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
        Datos dat = new Datos();
        Conexion con = new Conexion();
        public DataSet EstadoCampanias()
        {
            try
            {
                return dat.CampaniasMarketing();
            }
            catch (Exception ex)
            {
                Bitacora.Error("EstadoCampanias()", "Negocios.cs", ex);
                BitacoraText.Error("EstadoCampanias() ", "Negocios.cs", ex);
                return null;
            }
        }

        public string NombreImagen(Guid Id)
        {
            try
            {
                DataSet ds = dat.BuenDiaImagen(Id);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["NombreImagen"].ToString();
                }
                else
                {
                    return "";
                }
                
            }
            catch (Exception ex)
            {
                Bitacora.Error("NombreImagen()", "Negocios.cs", ex);
                BitacoraText.Error("NombreImagen() ", "Negocios.cs", ex);
                return null;
            }
        }

        public string DatosCliente(Guid Id)
        {
            try
            {
                DataSet ds = dat.DatosCliente(Id);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Nombre"].ToString()+","+ dt.Rows[0]["CIC"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                Bitacora.Error("DatosCliente()", "Negocios.cs", ex);
                BitacoraText.Error("DatosCliente() ", "Negocios.cs", ex);
                return null;
            }
        }
        public int _RegistraGestion(string IdEntidad, int Respuesta, double Monto, int Probabilidad, string Fecha,string Asignado, string DomainUser, string IdPadre, string Descripcion)
        {
            try
            {
                return dat.RegistraGestion(IdEntidad, Respuesta, Monto, Probabilidad, Fecha, Asignado, DomainUser, IdPadre, Descripcion);
                
            }
            catch (Exception ex)
            {
                Bitacora.Error("_RegistraGestion", "Negocios.cs", ex);
                BitacoraText.Error("_RegistraGestion", "Negocios.cs", ex);
                return -1;
            }
        }

        public DataSet LogCampanias(string CodigoCampania)
        {
            try
            {
                return dat.LogMarketing(CodigoCampania);
            }
            catch (Exception ex)
            {
                Bitacora.Error("LogCampanias(" + CodigoCampania + ")", "Negocios.cs", ex);
                BitacoraText.Error("LogCampanias(" + CodigoCampania + ") ", "Negocios.cs", ex);
                return null;
            }
        }

        public DataSet ControlEjecucion(string Accion)
        {
            try
            {
                return dat.ControlEjecucion(Accion);
            }
            catch (Exception ex)
            {
                Bitacora.Error("ControlEjecucion(" + Accion + ")", "Negocios.cs", ex);
                BitacoraText.Error("ControlEjecucion(" + Accion + ") ", "Negocios.cs", ex);
                return null;
            }
        }
        public DataSet LogCampaniasCarga(string CodigoCampania)
        {
            try
            {
                return dat.LogMarketingCarga(CodigoCampania);
            }
            catch (Exception ex)
            {
                Bitacora.Error("LogCampaniasCarga(" + CodigoCampania + ")", "Negocios.cs", ex);
                BitacoraText.Error("LogCampaniasCarga(" + CodigoCampania + ") ", "Negocios.cs", ex);
                return null;
            }
        }

        public void persona()
        {
            try
            {
                OrganizationServiceProxy CRMproxy;
                CRMproxy = con.ObtenerOrganizationService();

                ColumnSet columnas = new ColumnSet(new string[] { "firstname", "lastname" });
                Guid id = new Guid("03E4E440-9B1A-E311-ADA0-005056BA6B48");
                Contact contacto = (Contact)CRMproxy.Retrieve(Contact.EntityLogicalName, id, columnas);
            }
            catch (Exception ex)
            {
                Bitacora.Error("persona() ", "Negocios.cs", ex);
                BitacoraText.Error("persona() ", "Negocios.cs", ex);
            }
        }

        public DataTable _ObtenerCabecera(string _codigoCampania)
        {
            try
            {
                return dat.ObtenerCabecera(_codigoCampania);
            }
            catch (Exception ex)
            {
                Bitacora.Error("_ObtenerCabecera", "Error Controlado ", ex);
                BitacoraText.Error("_ObtenerCabecera", "Error Controlado", ex);
                return null;
            }
        }
        public DataTable _ObtenerDetalle(string _codigoCampania)
        {
            try
            {
                return dat.ObtenerDetalle(_codigoCampania);
            }
            catch (Exception ex)
            {
                Bitacora.Error("_ObtenerDetalle", "Error Controlado ", ex);
                BitacoraText.Error("_ObtenerDetalle", "Error Controlado", ex);
                return null;
            }
        }

        public DataTable _ObtenerAgenda(string IdEntidad, string DomainName)
        {
            try
            {
                return dat.ObtenerAgenda(IdEntidad, DomainName);
            }
            catch (Exception ex)
            {
                Bitacora.Error("_ObtenerAgenda", "Error Controlado ", ex);
                BitacoraText.Error("_ObtenerAgenda", "Error Controlado", ex);
                return null;
            }
        }
        public DataTable _ActualizacionPropietarios()
        {
            try
            {
                return dat.ActualizacionPropietarios();
            }
            catch (Exception ex)
            {
                Bitacora.Error("_ActualizacionPropietarios", "Error Controlado ", ex);
                BitacoraText.Error("_ActualizacionPropietarios", "Error Controlado", ex);
                return null;
            }
        }
        public DataTable _ActualizacionDatosCliente()
        {
            try
            {
                return dat.ActualizacionDatosCliente();
            }
            catch (Exception ex)
            {
                Bitacora.Error("_ActualizacionDatosCliente", "Error Controlado ", ex);
                BitacoraText.Error("_ActualizacionDatosCliente", "Error Controlado", ex);
                return null;
            }
        }
        public DataTable _CierreActividadesMarketing()
        {
            try
            {
                return dat.CierreActividadesMarketing();
            }
            catch (Exception ex)
            {
                Bitacora.Error("_CierreActividadesMarketing", "Error Controlado ", ex);
                BitacoraText.Error("_CierreActividadesMarketing", "Error Controlado", ex);
                return null;
            }
        }
        public DataTable _DatosLogEjecucion(int tipo, string codigoCampania)
        {
            try
            {
                return dat.DatosLogEjecucion(tipo, codigoCampania);
            }
            catch (Exception ex)
            {
                Bitacora.Error("_DatosLogEjecucion", "Error Controlado ", ex);
                BitacoraText.Error("_DatosLogEjecucion", "Error Controlado", ex);
                return null;
            }
        }
        public string _EjecucionProceso(string codigoCampania)
        {
            string mensaje = "Proceso ejecutado con exito";

            try
            {
                return dat.EjecucionProceso(codigoCampania);
            }
            catch (Exception ex)
            {
                Bitacora.Error("_EjecucionProceso", "Error Controlado ", ex);
                BitacoraText.Error("_EjecucionProceso", "Error Controlado", ex);
                mensaje = ex.Message;
            }
            return mensaje;
        }

        public string EjecucionEliminacion(string Proceso, string Accion, string Fecha)
        {
            string mensaje = "";

            try
            {
                return dat.EjecucionEliminacion(Proceso, Accion, Fecha);
            }
            catch (Exception ex)
            {
                Bitacora.Error("EjecucionEliminacion", "Error Controlado ", ex);
                BitacoraText.Error("EjecucionEliminacion", "Error Controlado", ex);
                mensaje = ex.Message;
            }
            return mensaje;
        }
    }
}
