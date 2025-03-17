namespace MICRE_APP_EXCEPCIONES.Models
{
    public class RegisterRespladoRequest
    {
        public int IdCliente { get; set; }
        public string? TipoRespaldo { get; set; }
        public string? Data { get; set; }
        public string? Matricula { get; set; }
        public string? NombreArchivo { get; set; }
        public string? TipoArchivo { get; set; }
    }

    public class DocumentoRespaldoExcepcionRequest : RegisterRespladoRequest
    {
        public int IdExcepcion { get; set; }
    }
}
