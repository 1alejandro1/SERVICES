namespace MICRE_APP_EXCEPCIONES.Models
{
    public class UpdateExceptionRequest
    {
        public string? Matricula { get; set; }
        public int IdExcepcion { get; set; }
        public int IdProductoExcepcion { get; set; }
        public string? Justificacion { get; set; }
    }
}
