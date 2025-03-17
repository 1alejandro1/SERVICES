using MICRE_APP_EXCEPCIONES.Models;

namespace NETCORE.Models
{
    public class FormInmuebleData
    {
        public int? idCliente { get; set; }
        public int? idClienteConyugue { get; set; }
        public List<ProductoInmuebleResponse>? idProductos { get; set; }
    }
}
