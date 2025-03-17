using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BCP.CROSS.REPORTES
{
    public class Reporte : IReporte
    {
        private readonly ReporteSettings _reporteSettings;
        public Reporte(IOptions<ReporteSettings> reporteSettings)
        {
            _reporteSettings = reporteSettings.Value; 
        }
        public Task<string> GetReporteAsync(object request,string tipoReporte)
        {
            string intermedio = JsonSerializer.Serialize(request);
            Dictionary<string,string> diccionario= JsonSerializer.Deserialize<Dictionary<string,string>>(intermedio);
            
            
            string path = Path.Combine(_reporteSettings.PathReporte, GetTipoReporte(tipoReporte));
            var PaginaHTML_Texto = File.ReadAllText(path);
            string valor =string.Empty;
            string espacios = string.Empty;
            foreach(var item in diccionario)
            {
                valor = item.Value;
                espacios = string.Empty;
                while (valor.Length>1&& valor[0].Equals(' '))
                {
                    espacios = espacios + "&ensp;";
                    if (valor.Length > 1) 
                    {
                        valor=valor.Substring(1);
                    }
                    else
                    {
                        valor = string.Empty;
                        break;
                    }
                }
                PaginaHTML_Texto = PaginaHTML_Texto.Replace($"@{item.Key}", espacios+valor);
            }          
            return Task.FromResult(PaginaHTML_Texto.ToString());
        }
        private string GetTipoReporte(string tipo)
        {
            string response = string.Empty;
            switch (tipo)
            {
                case "C":
                    response=_reporteSettings.ReporteConsulta;
                    break;
                case "S":
                    response = _reporteSettings.ReporteSolicitud;
                    break;
                case "R":
                    response = _reporteSettings.ReporteReclamo;
                    break;
            }
            return response;
        }
    }
}
