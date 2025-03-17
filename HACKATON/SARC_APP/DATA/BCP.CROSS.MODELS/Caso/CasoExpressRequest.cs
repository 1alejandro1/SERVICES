using System;
using System.Collections.Generic;
using System.Linq;

namespace BCP.CROSS.MODELS.Caso
{

    public class CasoExpressRequest
    {
        public bool EsCobro { get; set; }
        public bool EsDevolucion { get; set; }
        public CreateCasoExpressDTO Caso { get; set; }
        public DevolucionRequest Devolucion { get; set; }
        public CobroRequest Cobro { get; set; }

    }


}