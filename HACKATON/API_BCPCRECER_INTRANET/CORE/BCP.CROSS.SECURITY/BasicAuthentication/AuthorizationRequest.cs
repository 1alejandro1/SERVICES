using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.SECURITY.BasicAuthentication
{
    public class AuthorizationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ChanelAuthorized ChanelAuthorized { get; set; }
    }

    public class ChanelAuthorized
    {
        public string Date { get; set; }
        public string Channel { get; set; }
        public string PublicToken { get; set; }
        public string AppUserId { get; set; }
        public string PrivateToken { get; set; }
    }
}
