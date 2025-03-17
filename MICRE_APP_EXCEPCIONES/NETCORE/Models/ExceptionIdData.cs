namespace MICRE_APP_EXCEPCIONES.Models
{
    public class ExceptionIdData
    {
        public int IdProductoExcepcion { get; set; }
        public string? Justificacion { get; set; }
        public int IdExcepcion { get; set; }
        public string? DescripcionExcepcion { get; set; }
        public int IdProducto { get; set; }
        public int IdFinalidad { get; set; }
        public string? DescripcionFinalidad { get; set; }
        public string? DescripcionProducto { get; set; }
        public decimal MontoSolicitado { get; set; }
        public decimal MontoSolicitadoUSD { get; set; }
        public bool Garantia { get; set; }
        public string? DescripcionInmueble { get; set; }
        public decimal ValorInmueble { get; set; }
        public decimal ValorInmuebleUSD { get; set; }
        public string? Estado { get; set; }
        public string? MotivoAprobacion { get; set; }
        public string? MotivoRechazo { get; set; }
        public string? UsuarioAR { get; set; }
        public DateTime? FechaAprobacionRechazo { get; set; }
        public List<string> Observaciones { get; set; }
    }
}
