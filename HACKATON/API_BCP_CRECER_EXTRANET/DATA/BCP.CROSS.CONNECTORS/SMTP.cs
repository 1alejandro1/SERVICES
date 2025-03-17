using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.CONNECTORS
{
    public class SMTP
    {
        private IConfiguration Configuration { get; }
        private string _servidor;
        private int _puerto;

        private MailMessage correo = null;
        private SmtpClient smtp = null;

        public SMTP(IConfiguration configuration)
        {
            Configuration = configuration;
            this._servidor = Configuration["ConnectorSettings:SMTP:Server"];
            this._puerto = Convert.ToInt32(Configuration["ConnectorSettings:SMTP:Port"]);
        }

        public async Task<bool> EnviarCorreo(string from, string to, string subject, string content)
        {
            bool response = false;
                smtp = new SmtpClient(this._servidor);
                smtp.Port = this._puerto;

                correo = new MailMessage(from, to);
                correo.IsBodyHtml = true;
                correo.Subject = subject;
                correo.Body = content;

                await smtp.SendMailAsync(correo).ConfigureAwait(false);
                response = true;

            return response;
        }
    }
}
