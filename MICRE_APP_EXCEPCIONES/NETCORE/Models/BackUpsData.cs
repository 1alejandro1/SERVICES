namespace MICRE_APP_EXCEPCIONES.Models
{
    public class BackUpsData
    {
        public int IdRespaldo { get; set; }
        public int IdCliente { get; set; }
        public string? RespaldoDescripcion { get; set; }
        public string? NombreArchivo { get; set; }
        public string? TipoArchivo { get; set; }
        public string? TipoRespaldo { get; set; }
        public string? Url { get; set; }
    }
}
