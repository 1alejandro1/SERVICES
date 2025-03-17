using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCP.CROSS.MODELS.Segurinet
{
    public class ApiSegurinetRequest
    {
        public string application { get; set; }
        [Required(ErrorMessage = "Introduzaca su matricula")]
        public string user { get; set; }
        [Required(ErrorMessage = "Password requerido")]
        public string password { get; set; }
        public List<string> lstPolicies { get; set; }
        public string Msj { get; set; }
    }
}
