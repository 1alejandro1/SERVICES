using System.Collections.Generic;

namespace BCP.CROSS.COMMON
{
    public class SegurinetSettings
    {
        public List<Politics> Politicas { get; set; }
        public class Politics
        {
            public string nombre { get; set; }
            public string roles { get; set; }
        }
    
    }
  
}
