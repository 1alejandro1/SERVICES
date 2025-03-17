using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BCP.CROSS.MODELS.Client
{
    public class GetClientByIdcRequest
    {
        [Required(ErrorMessage ="Campo requerido")]
        [MaxLength(10)]
        public string Idc { get; set; }

        [Required]
        public string TipoIdc { get; set; }

        [Required]
        [MaxLength(3)]
        public string Extension { get; set; }

        public string Funcionario { get; set; }
    }

    public class GetClientByDbcRequest
    {
        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(200)]
        public string PaternoRazonSocial { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
    }
}
