namespace NETCORE.Models
{
    public class SolicitudRow
    {
        public int rowId { get; set; }
        public string? cI { get; set; }
        public string? nombreCliente { get; set;}
        public string? producto { get; set; }
        public string? estado { get; set; }
        public DateTime fechaIngreso { get; set;}
        public DateTime fechaModificacion { get; set; }
    }
}
