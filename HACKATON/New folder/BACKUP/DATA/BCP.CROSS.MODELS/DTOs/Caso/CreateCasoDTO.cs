using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class CreateCasoDTO
    {
        [Required]
        public string FuncionarioRegistro { get; set; }

        [Required]
        public string ClienteIdc { get; set; }

        [Required]
        [MaxLength(1)]
        public string ClienteIdcTipo { get; set; }

        [Required]
        [MaxLength(3)]
        public string ClienteIdcExtension { get; set; }

        [Required]
        public string ProductoId { get; set; }

        [Required]
        public string ServicioId { get; set; }

        [Required]
        public string Paterno { get; set; }

        public string Materno { get; set; }

        public string Nombres { get; set; }

        [Required]
        public string Empresa { get; set; }

        public string Sucursal { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }

        public string Agencia { get; set; }

        public string NroCuenta { get; set; }
        public string NroTarjeta { get; set; }
        public Decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string FechaTxn { get; set; }
        public string HoraTxn { get; set; }
        public string InformacionAdicional { get; set; }
        public string AtmSucursal { get; set; }
        public string AtmUbicacion { get; set; }
        public string DocumentosAdjuntoIn { get; set; }

        [Required]
        [RegularExpression(@"^[123]{1}$", ErrorMessage = "Solo las vias 1, 2 o 3")]
        public int ViaEnvioCodigo { get; set; }

        [Required]
        public string ViaEnvioRespuesta { get; set; }

        [Required]
        [RegularExpression(@"^[01]{1}$", ErrorMessage = "El valor debe ser 0 o 1")]
        public string SwComunicacionEnOficina { get; set; }

        [Required]
        [RegularExpression(@"^[01]{1}$", ErrorMessage = "El valor debe ser 0 o 1")]
        public string SwComunicacionEmail { get; set; }

        [Required]
        [RegularExpression(@"^[01]{1}$", ErrorMessage = "El valor debe ser 0 o 1")]
        public string SwComunicacionWhatsapp { get; set; }

        public string DireccionRespuesta { get; set; }

        public string TelefonoRespuesta { get; set; }

        [Required]
        public string EmailRespuesta { get; set; }

        [Required]
        public string SmsRespuesta { get; set; }

        public Decimal ImporteDevolucion { get; set; }
        public string MonedaDevolucion { get; set; }

        [Required]
        public string RutaSharePoint { get; set; }

        [Required]
        [MaxLength(1)]
        public string Canal { get; set; }

        public CreateCasoDTO()
        {
            MonedaDevolucion = string.Empty;
            InformacionAdicional = string.Empty;
            AtmSucursal = string.Empty;
            AtmUbicacion = string.Empty;
            DocumentosAdjuntoIn = string.Empty;
        }
    }
}
