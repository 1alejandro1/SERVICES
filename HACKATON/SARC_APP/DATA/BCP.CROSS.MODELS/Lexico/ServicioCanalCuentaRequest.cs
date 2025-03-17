using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Lexico
{
    public class ServicioCanalCuentaRequest
    {
        public string IdProducto { get; set; }
        public string IdServicio { get; set; }
        public int CuentaSelected { get; set; }
    }
}
