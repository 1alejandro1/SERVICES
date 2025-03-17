using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes.Requests
{
    public class CRNDetalleRequest
    {
        public CRNDetalleRequest(string fechaIni, string fechaFin, string tipo)
        {
            FechaIni = fechaIni;
            FechaFin = fechaFin;
            Tipo = tipo;
        }

        [Required]
        public string FechaIni { get; set; }

        [Required]
        public string FechaFin { get; set; }
        public string Tipo { get; set; }
    }
}
