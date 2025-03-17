using System.Collections.Generic;

namespace BCP.CROSS.SEGURINET
{
    public class SegurinetSettings
    {
        public string UriSegurinet { get; set; }
        public string UriSegurinet2 { get; set; }
        public string MethodLogin { get; set; }
        public string MethodChangePassword { get; set; }
        public bool Segurinet2 { get; set; }
        public string AplicationName { get; set; }
        public List<Politica> politicas { get; set; }
    }
    public class Politica
    {
        public string nombre { get; set; }
        public List<string> roles { get; set; }
    }
}