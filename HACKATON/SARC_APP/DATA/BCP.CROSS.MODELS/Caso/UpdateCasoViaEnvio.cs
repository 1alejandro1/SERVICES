using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Caso
{
    public class UpdateCasoViaEnvio
    {

        public string NroCarta { get; set; }

        [Required]
        [Display(Name = "Vía envío Respuesta")]
        public string ViaEnvio { get; set; }
        public string RespuestaEnviada { get; set; }
        public string ComTelefono { get; set; }
        public string ComSMS { get; set; }
        public string ComOficina { get; set; }
        public string ComEmail { get; set; }
        public string ComWhastapp { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [MaxLength(100)]
        public string Direccion { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [MaxLength(50)]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Celular")]
        [MaxLength(20)]
        public string SMS { get; set; }

        public UpdateCasoViaEnvio()
        {
            RespuestaEnviada = "0";
            ComTelefono = "0";
            ComSMS = "0";
        }
    }
}
