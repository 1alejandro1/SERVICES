using System;
using System.ComponentModel.DataAnnotations;

namespace BCP.CROSS.MODELS.Reportes
{
    public class ReporteRequest
    {
        public ReporteRequest(string fechaIni, string fechaFin)
        {
            FechaIni = fechaIni;
            FechaFin = fechaFin;
        }

        [Required]
        public string FechaIni { get; set; }

        [Required]
        public string FechaFin { get; set; }
    }
}
