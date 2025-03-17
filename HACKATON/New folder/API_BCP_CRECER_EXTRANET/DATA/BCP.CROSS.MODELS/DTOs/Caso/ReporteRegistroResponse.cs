using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class ReporteRegistroResponse
    {
        public string Usuario { get; set; }
        public string FechaRegistro { get; set; }
        public string NroCaso { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string ClienteIdc { get; set; }
        public string Sucursal { get; set; }
        public string Agencia { get; set; }
        public string Direccion { get; set; }
        public string NombreEmpresa { get; set; }
        public string IdProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string NroCuenta { get; set; }
        public string NroTarjeta { get; set; }
        public string Monto { get; set; }
        public string Moneda { get; set; }
        public string DescripcionCaso { get; set; }
        public string DocumentacionAdjunta { get; set; }
        public string ClasificacionReclamo { get; set; }
        public string AreaAtencionReclamo { get; set; }
        public string FuncionarioAtencionReclamo { get; set; }
    }
}
