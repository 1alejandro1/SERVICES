using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Configuration;
using System.Net.Mail;
using NLog.Layouts;
using System.IO;
using System.Reflection;

namespace Rbt.Log
{
    public class Bitacora
    {
        private static string CorreoErrores = ConfigurationManager.AppSettings["CORREO_OPERACIONES"];
        private static string destinatario = ConfigurationManager.AppSettings["CORREO_OPERACIONES"];
        private static string CorreoDe = ConfigurationManager.AppSettings["CORREO_APLICATIVO"];
        private static string ServidorCorreo = ConfigurationManager.AppSettings["SERVIDOR_SMTP"];
        private static string Asunto = ConfigurationManager.AppSettings["ASUNTO"];
        private static string Cuerpo = ConfigurationManager.AppSettings["CORREO_CUERPO"];

        protected static void configuracion()
        {
            LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
            FileTarget fileTarget = new FileTarget();
            fileTarget.FileName = (Layout)(ConfigurationManager.AppSettings["RUTA_LOG"].ToString() + ".txt");
            fileTarget.Layout = (Layout)ConfigurationManager.AppSettings["LAYOUT_LOG"].ToString();
            loggingConfiguration.AddTarget("logfile", (Target)fileTarget);
            LoggingRule loggingRule = new LoggingRule("*", NLog.LogLevel.Info, (Target)fileTarget);
            loggingConfiguration.LoggingRules.Add(loggingRule);
            LogManager.Configuration = loggingConfiguration;
        }

        public static void LogInformacion(Logger logger, string descripcion)
        {
            try
            {
                Bitacora.configuracion();
                logger.Info("   Informacion:    -> " + descripcion);
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException($"[LogInformacion] {ex.Message}");
        
            }
        }

        public static void LogAdvertencia(Logger logger, string descripcion)
        {
            try
            {
                Bitacora.configuracion();
                logger.Info("   Advertencia:    -> " + descripcion);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"[LogInformacion] {ex.Message}");

            }
        }

        public static void LogError(Logger logger, string descripcion, Exception error)
        {
            try
            {
                Bitacora.configuracion();
                logger.Info("   Error      :    " + descripcion + " -> " + error.ToString());
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"[LogInformacion] {ex.Message}");

            }
        }

        public static void Enviarmails(string para, string de, string asunto, string cuerpo, string ServidorCorreo)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(de);
            para = para.Trim(Convert.ToChar(32));
            message.To.Add(para);
            message.Subject = asunto;
            message.Body = cuerpo;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.Normal;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = ServidorCorreo;
            smtpClient.Port = 25;
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Bitacora.LogError(LogManager.GetCurrentClassLogger(), "Error en el envio de notificación al mail: " + para, ex);
            }
        }

        public static void EnviarmailsGenerico(string cuerpodetalle, string servicioCorreoPara)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Bitacora.CorreoDe);
            message.To.Add(servicioCorreoPara);
            message.Subject = Bitacora.Asunto;
            message.Body = Bitacora.Cuerpo + ": " + cuerpodetalle;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.Normal;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = Bitacora.ServidorCorreo;
            smtpClient.Port = 25;
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Bitacora.LogError(LogManager.GetCurrentClassLogger(), "Error en el envio de notificación al mail: " + servicioCorreoPara, ex);
            }
        }

        public static string EnviarmailsVarios(List<string> Para, string de, string asunto, string cuerpo)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(de);
            for (int index = 0; index < Para.Count; ++index)
                message.To.Add(Para[index]);
            message.Subject = asunto;
            message.Body = cuerpo;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = Bitacora.ServidorCorreo;
            smtpClient.Port = 25;
            try
            {
                smtpClient.Send(message);
                return "";
            }
            catch (Exception ex)
            {
                return "Error en el envio de notificación por el siguiente error " + ex.Message;
            }
        }


        public static void EnviarmailsOperaciones(string cuerpo)
        {
            //Armado del correo
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(CorreoDe);
            CorreoErrores = CorreoErrores.Trim(Convert.ToChar(32));
            correo.To.Add(CorreoErrores);
            correo.Subject = Asunto;
            correo.Body = cuerpo;
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            // Declaracion del servidor
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = ServidorCorreo;
            smtp.Port = 25;
            //Envio de mail
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Bitacora.LogError(LogManager.GetCurrentClassLogger(), "Error en el envio de notificación al mail: " + CorreoErrores, ex);
            }
        }
        public static void EnviarmailsDestinatario(string cuerpo)
        {
            //Armado del correo
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(CorreoDe);
            destinatario = destinatario.Trim(Convert.ToChar(32));
            correo.To.Add(destinatario);
            correo.Subject = Asunto;
            correo.Body = cuerpo;
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            // Declaracion del servidor
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = ServidorCorreo;
            smtp.Port = 25;
            //Envio de mail
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Bitacora.LogError(LogManager.GetCurrentClassLogger(), "Error en el envio de notificación al mail: " + CorreoErrores, ex);
            }
        }
        public static void EnviarmailsDestinatarioHTML(string cuerpo, string asunto, string para)
        {
            //Armado del correo
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(CorreoDe);
            destinatario = destinatario.Trim(Convert.ToChar(32));
            correo.To.Add((para.Trim().Length > 0) ? para : destinatario);
            correo.Subject = asunto;
            correo.Body = cuerpo;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            // Declaracion del servidor
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = ServidorCorreo;
            smtp.Port = 25;
            //Envio de mail
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Bitacora.LogError(LogManager.GetCurrentClassLogger(), "Error en el envio de notificación al mail: " + CorreoErrores, ex);
            }
        }
        public static void EnviarmailsHTML(string para, string de, string asunto, string cuerpo, string ServidorCorreo)
        {
            //Armado del correo
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(de);
            para = para.Trim(Convert.ToChar(32));
            correo.To.Add(para);
            correo.Subject = asunto;
            correo.Body = cuerpo;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            // Declaracion del servidor
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = ServidorCorreo;
            smtp.Port = 25;
            //Envio de mail
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                Bitacora.LogError(LogManager.GetCurrentClassLogger(), "Error en el envio de notificación al mail: " + CorreoErrores, ex);
            }
        }

        public static void RegistroErrorLogEmail(string ErrorDetalle, Exception error)
        {
            Bitacora.EnviarmailsGenerico(ErrorDetalle + " " + error.Message.ToString(), CorreoErrores);
            Bitacora.LogError(LogManager.GetCurrentClassLogger(), ErrorDetalle, error);
        }

        public static void RegistroinformacionLogEmail(string Detalle)
        {
            Bitacora.EnviarmailsGenerico(Detalle, CorreoErrores);
            Bitacora.LogAdvertencia(LogManager.GetCurrentClassLogger(), Detalle);
        }


        public static void registroTXT(string contenido)
        {
            try
            {
                string stringFecha=DateTime.Now.ToString("yyyyMMdd");
                string stringFechaLog = DateTime.Now.ToString("yyyy-MM-dd");
                string rutaLog = ConfigurationManager.AppSettings["RutaPantallas"].ToString()+ "\\"+ stringFechaLog + "\\"+ ConfigurationManager.AppSettings["NombreLogPantalla"] +"_"+ stringFecha + ".txt";
                if (!File.Exists(rutaLog))
                    File.Create(rutaLog).Dispose();
                StreamWriter escribir = File.AppendText(rutaLog);
                escribir.WriteLine(contenido);
                escribir.Flush();
                escribir.Close();
                escribir.Dispose();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(MethodBase.GetCurrentMethod() + "-->" + ex.ToString());
            }

        }
    }
}

