using System.ComponentModel.DataAnnotations;

namespace NETCORE.Models
{
    public class User
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "* Matricula reguerida")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "* Ingrese una contraseña")]
        public string? Password { get; set; }

    }
}
