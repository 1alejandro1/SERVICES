using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Caso
{
    public class CerrarCasoRequest
    {
        public string FuncionarioSupervisor { get; set; }
        public string NroCarta { get; set; }
        public string Producto { get; set; }
        public string Servicio { get; set; }

        [Display(Name ="Documentación")]
        public string Documento { get; set; }

        [Display(Name = "Error registro caso")]
        public string ErrorReg { get; set; }

        [Display(Name = "Tipo Error")]
        [Required]
        public string IdErrorReg { get; set; }

        [Display(Name = "Observación")]
        [Required]
        public string DescripcionError { get; set; }

        [Display(Name = "Número cartas enviadas")]
        public int CartasEnviadas { get; set; }
    }
}
