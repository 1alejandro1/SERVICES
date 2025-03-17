using System;
using System.ComponentModel.DataAnnotations;

namespace Sarc.WebApp.Models.Reportes
{
    public class CobrosDevolucionesViewModel
    {
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }

        public string Funcionario { get; set; }
        [Required]
        public int Canal { get; set; }
        [Required]
        public int Estado { get; set; }
        [Required]
        public int Tipo { get; set; }
    }
}
