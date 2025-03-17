using System.ComponentModel.DataAnnotations;

namespace NETCORE.Models
{
    public class ClienteBusqueda
    {
        [Required(ErrorMessage = "* La cédula de identidad es obligatoria.")]
        public string? Ci { get; set; }

        public string? Complemento { get; set; }

        public string? Extencion { get; set; }
    }
}
