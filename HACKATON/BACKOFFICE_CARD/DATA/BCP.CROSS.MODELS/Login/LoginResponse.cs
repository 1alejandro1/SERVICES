using System;
using System.Collections.Generic;

namespace BCP.CROSS.MODELS.Login
{
    public class LoginResponse
    {
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public List<string> Politicas { get; set; }
        public UserToken Token { get; set; }
    }
    public class RolUsuario
    {
        public string Nombre { get; set; }
        public List<string> Politicas { get; set; }
    }
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class Politica
    {
        public string nombre { get; set; }
        public List<string> roles { get; set; }
    }
}