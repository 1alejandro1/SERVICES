using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeSeguridad;
using Bitacoras;
using System.Configuration;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Web.Services.Protocols;

namespace CapaDeDatos
{
    public class Conexion
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
        #region Variables
        Seguridad seg = new Seguridad();
        OrganizationServiceProxy CRMproxy;
        #endregion

        #region ConexionBD
        // CONEXION A BD
        public string strConexionBD()
        {
            string CadCnx = string.Empty;
            string strPassword = string.Empty;
            try
            {
                // conexión a la BD de CRM

                CadCnx = @"Data Source=" + ConfigurationManager.AppSettings["DataSource"] + ";";
                CadCnx += "Initial Catalog=" + ConfigurationManager.AppSettings["InitialCatalog"] + ";";
                CadCnx += "User Id=" + ConfigurationManager.AppSettings["UserId"] + ";";
                strPassword = seg.PasswordBD();
                CadCnx += "Password=" + strPassword;

            }
            catch (Exception ex)
            {
                Bitacora.Error("Conexion - strConexionBD()", "Conexion.cs", ex);
                BitacoraText.Error("Conexion - strConexionBD()", "Conexion.cs", ex);
                CadCnx = string.Empty;
            }
            return CadCnx;
        }
        #endregion

        #region ConexionCRM
        public OrganizationServiceProxy ObtenerOrganizationService()
        {

            try
            {
                string strPassword = string.Empty;
                string strPasswordEncriptado = string.Empty;
                string PasswordEncriptado = ConfigurationManager.AppSettings["PasswordEncriptado"].ToString();
                strPassword = seg.PasswordCRM();

                // conexión a CRM Dynamics 2011
                ClientCredentials credenciales = new ClientCredentials();

                credenciales.Windows.ClientCredential = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["CRMUser"].ToString(),
                    strPassword,
                    ConfigurationManager.AppSettings["CRMDominio"].ToString());

                CRMproxy = new OrganizationServiceProxy(
                    new Uri(ConfigurationManager.AppSettings["CRMURLService"].ToString()),
                    null,
                    credenciales,
                    null);
                CRMproxy.EnableProxyTypes();
                CRMproxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.CloseTimeout = new TimeSpan(1, 0, 0);
                CRMproxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.OpenTimeout = new TimeSpan(1, 0, 0);
                CRMproxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                CRMproxy.ServiceConfiguration.CurrentServiceEndpoint.Binding.SendTimeout = new TimeSpan(1, 0, 0);

                CRMproxy.Timeout = new TimeSpan(1, 0, 0);
                return CRMproxy;
            }
            catch (SoapException exSoap)
            {
                Bitacora.Error("Conexion - ObtenerOrganizationService()", "Conexion.cs", exSoap);
                BitacoraText.Error("Conexion - ObtenerOrganizationService()", "Conexion.cs", exSoap);
                return null;
            }
            catch (Exception ex)
            {
                Bitacora.Error("Conexion - ObtenerOrganizationService()", "Conexion.cs", ex);
                BitacoraText.Error("Conexion - ObtenerOrganizationService()", "Conexion.cs", ex);
                return null;
            }
        }
        #endregion
    }
}
