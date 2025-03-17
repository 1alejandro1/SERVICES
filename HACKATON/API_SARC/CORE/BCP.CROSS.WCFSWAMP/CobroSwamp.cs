using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.WCFSWAMP
{
    public class CobroSwamp: WCFSwampRequest
    {
        public string Correo { get; set; }
        public string Telefono { get; set; }
        #region cobro
        public int ID_COBRO { get; set; }
        public string DescripcionCobro { get; set; }
        
        #endregion
        #region Facturacion
        public bool FacturacionOnline { get; set; }
        public string FacturacionClave { get; set; }
        public string FacturacionDescripcion { get; set; }
        #endregion
    }
}
