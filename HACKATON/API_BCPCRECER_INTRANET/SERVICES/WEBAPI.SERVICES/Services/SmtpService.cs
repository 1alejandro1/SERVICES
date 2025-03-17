using BCP.CROSS.COMMON;
using BCP.CROSS.CONNECTORS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES
{
    public class SmtpService : ISmtpService
    {
        private readonly SMTP _smtp;
        public SmtpService(IConfiguration configuration)
        {
            _smtp = new SMTP(configuration);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<ServiceResponse<string>> EnviarCodigoCasoAsync(string codigoCaso, string emailCliente, string requestId)
        {
            var from = Configuration["ConnectorSettings:SMTP:From"];
            var cc = Configuration["ConnectorSettings:SMTP:CC"];
            var subject = Configuration["ConnectorSettings:SMTP:SubjectCodigo"];
            ServiceResponse<string> response = new();
            try
            {
                var body = BuildBody(codigoCaso);
                var responseSmtp = await _smtp.EnviarCorreo(from, $"{emailCliente},{cc}", subject, body);
                if (responseSmtp)
                {

                    response.Data = $"Codigo enviado a: {emailCliente}";
                    response.Meta = new Meta { Msj = "Success", ResponseId = requestId, StatusCode = 200 };
                    return response;
                }
                else
                {
                    response.Data = $"No se pudo enviar el correo a: {emailCliente}";
                    response.Meta = new Meta { Msj = "Not Found", ResponseId = requestId, StatusCode = 404 };
                }
            }
            catch (Exception ex)
            {
                response.Data = $"No se pudo enviar el correo a: {emailCliente}, error: {ex.Message}";
            }

            return response;

        }

        private static string BuildBody(string codigoReclamo)
        {
            StringBuilder body = new StringBuilder();

            body.AppendFormat(@"
                <!DOCTYPE html>
                <html lang='es'>
                    <head>
                        <meta charset='utf-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>
                    </head>
                    <body>
                        <p>Estimado(a) cliente,</p>
                        <p>
                            Le informamos que el código de su caso ingresado es:
                        </p>
                        <ul>
                            <li><strong>{0}</strong></li>
                        </ul>
                        <br>
                        <hr>
                       <footer style='color: #ee0909'>
                            <p> &copy; {1} - BANCO DE CREDITO DE BOLIVIA </p>
                       </footer>
                    </body>
                </html>", codigoReclamo, DateTime.Now.Year);

            return body.ToString();
        }
    }
}
