using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BCP.CROSS.MODELS.Reportes
{
    public class ReporteRequest
    {
        [Required]
        public string FechaIni { get; set; }

        [Required]
        public string FechaFin { get; set; }
    }
}
