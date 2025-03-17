using System.Collections.Generic;

namespace BCP.CROSS.SEGURINET.Services
{
    public class LoginServiceResponseData 
    {
        public List<string> ItemsPoliticas { get; set; }
        public string Login { get; set; }
        public string NombreUsuario { get; set; }
        public string Roles { get; set; }
    }
}
