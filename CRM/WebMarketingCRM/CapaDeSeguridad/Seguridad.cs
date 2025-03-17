using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Bitacoras;
using Encriptador;
namespace CapaDeSeguridad
{
    public class Seguridad
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

        public string PasswordBD()
        {
            try
            {
                string strPassword = string.Empty;
                string strPasswordEncriptado = ConfigurationManager.AppSettings["PasswordEncriptado"].ToString();
                if (strPasswordEncriptado.Equals("SI"))
                {
                    strPassword = "";
                    strPasswordEncriptado = ConfigurationManager.AppSettings["Password"].ToString();
                    
                    Encryptador.EncryptDecrypt(false, strPasswordEncriptado, ref strPassword);
                }
                else
                {
                    strPassword = ConfigurationManager.AppSettings["Password"].ToString();
                }
                return strPassword;
            }
            catch (Exception ex)
            {
                Bitacora.Error("Seguridad - PasswordBD()", "Error Controlado ", ex);
                BitacoraText.Error("Seguridad - PasswordBD()", "Error Controlado", ex);
                return "Error";
            }
        }

        public string PasswordCRM()
        {
            try
            {
                string strPassword = string.Empty;
                string strPasswordEncriptado = ConfigurationManager.AppSettings["PasswordEncriptado"].ToString();
                if (strPasswordEncriptado.Equals("SI"))
                {
                    strPassword = "";
                    strPasswordEncriptado = ConfigurationManager.AppSettings["CRMPassword"].ToString();
                    
                    Encryptador.EncryptDecrypt(false, strPasswordEncriptado, ref strPassword);
                }
                else
                {
                    strPassword = ConfigurationManager.AppSettings["CRMPassword"].ToString();
                }
                return strPassword;
            }
            catch (Exception ex)
            {
                Bitacora.Error("Seguridad - PasswordBD()", "Error Controlado ", ex);
                BitacoraText.Error("Seguridad - PasswordBD()", "Error Controlado", ex);
                return "Error";
            }
        }
    }
}
