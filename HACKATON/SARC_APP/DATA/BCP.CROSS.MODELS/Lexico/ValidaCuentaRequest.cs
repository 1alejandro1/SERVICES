using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Lexico
{
    public class ValidaCuentaRequest
    {
        public string IDC { get; set; }
        public string IdcTipo { get; set; }
        public string IdcExtenion { get; set; }
        public string NroCuenta { get; set; }
    }

    public class ValidaCuentaResponse
    {
        public string CuentaRepExt { get; set; }
    }

}
