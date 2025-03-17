namespace NETCORE.Models.APIResponse
{
    public class Excepcion
    {
        public int IdDetalle { get; set; }
        public string? Descripcion { get; set; }
        public int IdTipo { get; set; }
        public string? DescripcionTipo { get; set; }
    }
}