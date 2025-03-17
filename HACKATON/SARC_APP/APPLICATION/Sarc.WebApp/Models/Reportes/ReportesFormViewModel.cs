using System;
using System.ComponentModel.DataAnnotations;

namespace Sarc.WebApp.Models.Reportes
{
    public class ReportesFormViewModel
    {
        public string ASFITipo { get; set; }
        public string AnalistaTipo { get; set; }
        public string CNRTipo { get; set; }
        [Required]
        public DateTime FechaIni { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }

    }
}
