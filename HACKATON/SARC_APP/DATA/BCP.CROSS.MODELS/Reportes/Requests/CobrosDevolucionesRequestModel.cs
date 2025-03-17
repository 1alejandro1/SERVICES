using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes.Requests
{
    public class CobrosDevolucionesRequestModel
    {
        [Required]
        public string FechaInicio { get; set; }
        [Required]
        public string FechaFin { get; set; }
        
        public string Funcionario { get; set; }
        [Required]
        public int Canal { get; set; }
        [Required]
        public int Estado { get; set; }
        [Required]
        public int Tipo { get; set; }
    }
}
