using System.ComponentModel.DataAnnotations;

namespace BCP.CROSS.MODELS
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Introduzaca su matricula")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Password requerido")]
        public string Password { get; set; }
        public string Msj { get; set; }
    }
}