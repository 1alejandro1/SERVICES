using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace Bitacoras
{
    public class GestorCorreos
    {
        static MailMessage correo = null;
        static SmtpClient smtp = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="cuerpo"></param>
        /// <param name="flagHtml"></param>
        public static void EnvioCorreo(string destinatario, string asunto, string cuerpo, bool flagHtml)
        {
            Correo entidad = new Correo();
            entidad.Destinatario = destinatario;
            entidad.FlagHtml = flagHtml;
            entidad.Asunto = asunto;
            entidad.Cuerpo = cuerpo;
            CargaCorreo(entidad);
            smtp.Send(correo);
        }

        public static void EnvioCorreoEx(string destinatario, string asunto, string cuerpo, bool flagHtml)
        {
            Correo entidad = new Correo();
            entidad.Destinatario = destinatario;
            entidad.FlagHtml = flagHtml;
            entidad.Asunto = asunto;
            entidad.Cuerpo = cuerpo;
            CargaCorreoEx(entidad);
            smtp.Send(correo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidadCorreo"></param>
        /// 

        public static void CargaCorreo(Correo entidadCorreo)
        {
            correo = new MailMessage();
            smtp = new SmtpClient();
            correo.To.Clear();
            correo.To.Add(entidadCorreo.Destinatario);
            correo.IsBodyHtml = entidadCorreo.FlagHtml;
            correo.Subject = entidadCorreo.Asunto;
            correo.Body = entidadCorreo.Cuerpo;
        }
        public static void CargaCorreoEx(Correo entidadCorreo)
        {
            correo = new MailMessage(ConfigurationManager.AppSettings.Get("CORREO_ORIGEN"), ConfigurationManager.AppSettings.Get("CORREO_DESTINO"));
            smtp = new SmtpClient();
            correo.To.Clear();
            correo.To.Add(entidadCorreo.Destinatario);
            correo.IsBodyHtml = entidadCorreo.FlagHtml;
            correo.Subject = entidadCorreo.Asunto;
            correo.Body = entidadCorreo.Cuerpo;
        }
    }
    public class Correo
    {
        private string destinatario;
        public string Destinatario
        {
            get { return destinatario; }
            set { destinatario = value; }
        }
        private string asunto;
        public string Asunto
        {
            get { return asunto; }
            set { asunto = value; }
        }
        private string cuerpo;
        public string Cuerpo
        {
            get { return cuerpo; }
            set { cuerpo = value; }
        }
        private bool flagHtml;
        public bool FlagHtml
        {
            get { return flagHtml; }
            set { flagHtml = value; }
        }
	
    }
}
