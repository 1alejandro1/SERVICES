using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCP.CROSS.MODELS.Caso
{
    public class CreateCasoDTO
    {
        [Required]
        public string FuncionarioRegistro { get; set; }

        [Required]
        [Display(Name = "Tipo Registro")]
        public string TipoRegistro { get; set; }

        public string NombreUsuario { get; set; }

        [Required]
        public string ClienteIdc { get; set; }

        [Required]
        [MaxLength(1)]
        public string ClienteIdcTipo { get; set; }

        [Required]
        [MaxLength(3)]
        public string ClienteIdcExtension { get; set; }

        [Required]
        [Display(Name = "Producto")]
        public string ProductoId { get; set; }

        [Required]
        [Display(Name = "Servicio")]
        public string ServicioId { get; set; }

        [Required]
        public string Paterno { get; set; }

        public string Materno { get; set; }

        public string Nombres { get; set; }

        [Required]
        public string Empresa { get; set; }

        public string Sucursal { get; set; }
        public string Departamento { get; set; }

        [MaxLength(100)]
        public string Ciudad { get; set; }

        public string Agencia { get; set; }

        [MaxLength(22, ErrorMessage = "Máximo 20 caracteres")]
        [Display(Name = "Número Cuenta")]
        //[Remote(action: "ValidateCuentaRepext", controller: "Casos", AdditionalFields = nameof(ClienteIdc) + "," + nameof(ClienteIdcTipo) + "," + nameof(ClienteIdcExtension))]
        public string NroCuenta { get; set; }

        [MaxLength(22, ErrorMessage = "Máximo 20 caracteres"), MinLength(14, ErrorMessage = "MÍnimo 16 caracteres")]
        [Display(Name = "Número Tarjeta")]
        public string NroTarjeta { get; set; }
        

[Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Formato invalido")]
        [Display(Name = "Monto TXN")]
        public Decimal Monto { get; set; }

        [Required]
        public string Moneda { get; set; }

        [Display(Name = "Fecha TXN")]
        public string FechaTxn { get; set; }

        [Display(Name = "Hora TXN")]
        public string HoraTxn { get; set; }

        [Display(Name = "Descripción Caso")]
        public string InformacionAdicional { get; set; }

        [Display(Name = "ATM Sucursal")]
        public string AtmSucursal { get; set; }

        [Display(Name = "ATM Ubicación")]
        public string AtmUbicacion { get; set; }

        [Display(Name = "Documentos Adjuntos")]
        public string DocumentosAdjuntoIn { get; set; }


        [Required]
        [Display(Name = "Vía envio Código")]
        [RegularExpression(@"^[123]{1}$", ErrorMessage = "Solo las vias 1, 2 o 3")]
        public int ViaEnvioCodigo { get; set; }

        [Required]
        [Display(Name = "Vía envío Respuesta")]
        public string ViaEnvioRespuesta { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100)]
        public string DireccionRespuesta { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [MaxLength(50)]
        public string TelefonoRespuesta { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(50)]
        public string EmailRespuesta { get; set; }

        [Required]
        [Display(Name = "Celular")]
        [MaxLength(20)]
        public string SmsRespuesta { get; set; }

        [Display(Name = "Importe Devolución")]
        public Decimal ImporteDevolucion { get; set; }

        [Display(Name = "Moneda Devolución")]
        public string MonedaDevolucion { get; set; }

        public string TipoCarta { get; set; }

        public List<ArchivoAdjunto> ArchivosAdjuntos { get; set; }

        public CreateCasoDTO()
        {
            MonedaDevolucion = string.Empty;
            InformacionAdicional = string.Empty;
            AtmSucursal = string.Empty;
            AtmUbicacion = string.Empty;
            DocumentosAdjuntoIn = string.Empty;
        }
    }

    public class ArchivoAdjunto
    {
        public string Nombre { get; set; }
        public string Archivo { get; set; }
    }
}

