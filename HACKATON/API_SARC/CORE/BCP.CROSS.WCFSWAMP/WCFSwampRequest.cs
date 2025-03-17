using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.WCFSWAMP
{
    public class WCFSwampRequest
    {
        public string Banca { get; set; }
        public string Centro { get; set; }//validar
        public string CIC { get; set; }
        public string Funcionario { get; set; }
        public string Supervisor { get; set; }
        public string NombreCompleto { get; set; }
        public string ClienteIdc { get; set; }
        public string NroCaso { get; set; }
        public string NroCuenta { get; set; }
        public string ClienteNit { get; set; }
        public string Moneda { get; set; }
        public string DatosFactura { get; set; }
        public string Direccion { get; set; }
        public string DescripcionTramite { get; set; }
        public decimal Monto { get; set; }

        public string Glosa { get; set; }
        #region CANAL
        public int ServiciosCanalesId { get; set; }
        public string CanalClave { get; set; }
        public string CanalCuenta { get; set; }
        public string CanalProducto { get; set; }
        #endregion
        public string ProductoId { get; set; }
        public string ServicioId { get; set; }
    }
}
