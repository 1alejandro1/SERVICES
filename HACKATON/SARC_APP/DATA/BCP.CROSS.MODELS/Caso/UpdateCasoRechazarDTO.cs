using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Caso
{
    public class UpdateCasoRechazarDTO
    {
        public string NroCaso { get; set; }
        public string SW { get; set; }

        [Required]
        [Display(Name ="Motivo Rechazo")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name ="Tipo de error")]
        public string TipoError { get; set; }

        public string FechaProrroga { get; set; }

        [Required]
        [Display(Name = "Fecha Prorroga")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}
