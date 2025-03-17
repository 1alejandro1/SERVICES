namespace NETCORE.Models
{
    public class ExceptionModel
    {
        public string? matricula { get; set; }
        public string? detalleBuscar { get; set; }
        public int idProductoExcepcion { get; set; }
        public string? Justificacion { get; set; }
        public List<string>? idProductos { get; set; }
    }
}