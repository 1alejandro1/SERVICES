using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCP.CROSS.MODELS.Generated
{
    public class GeneratedRequest
    {
        public string publicToken { get; set; }
        public string appUserId { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Documento de Identidad")]
        public string documentNumber { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string documentType { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string documentExtention { get; set; }
        public string documentComplement { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string cic { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string name { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string secondName { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string accountNumber { get; set; }
        public string accountType { get; set; }
        public string accountDescription { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string cardCommerceCode { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string cardCommerceId { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int serviceId { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string cellPhoneNumber { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [RegularExpression(@"\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Formato del email inválido")]
        public string email { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Fecha Nacimiento")]
        public string dateBirth { get; set; }
        
        public string address { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(1, ErrorMessage = "Máximo 1 caracteres")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Fecha Activación Tarjeta")]
        public string activationDate { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Fecha Expiración Tarjeta")]
        public string expirationDate { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string cardStatus { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(6, ErrorMessage = "Máximo 6 caracteres")]
        public string cardName { get; set; }
        public string ip { get; set; }
        public string identifier { get; set; }
        public string operatingSystem { get; set; }
        public string device { get; set; }
        public string browser { get; set; }
        public string channel { get; set; }
        public string domainUser { get; set; }
        public List<ArchivoAdjunto> ArchivosAdjuntos { get; set; }
        public string DocumentosAdjuntoIn { get; set; }
    }  
    public class ArchivoAdjunto
    {
        public string Nombre { get; set; }
        public string Archivo { get; set; }
    }
}
