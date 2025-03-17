namespace MICRE_APP_EXCEPCIONES.TDOs
{
    public class RegisterExceptionTDO
    {
        public string? matricula { get; set; }
        public int idProductoExcepcion { get; set; }
        public string? justificacion { get; set; }
        public List<string>? idProductos { get; set; }
    }
}
