using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS
{
    public class LoginResponse
    {
        public string Nombre { get; set; }   
        public string Matricula { get; set; }
        public List<string> Politicas { get; set; }
        public UserToken Token { get; set; }
    }
    public class UserToken
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
