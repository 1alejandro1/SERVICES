using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class CasoDTO
    {
        public string FechaRegistro { get; set; }
        public string NroCaso { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Idc { get; set; }
        public string Sucursal { get; set; }
        public string Agencia { get; set; }
        public string Direccion { get; set; }
        public string NombreEmpresa { get; set; }
        public string ProductoId { get; set; }
        public string Producto { get; set; }
        public string Cuenta { get; set; }
        public string Tarjeta { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public string DocumentacionAdjunta { get; set; }
        public string ViaEnvioCodigo {get; set;}
        public string ClasificacionCaso { get; set;}
        public string AtendidoPor {get; set;}
        public string AsignadoPor {get; set;}
    }
}
