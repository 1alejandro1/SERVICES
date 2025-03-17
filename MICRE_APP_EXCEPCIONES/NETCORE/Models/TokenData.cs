
namespace NETCORE.Models
{
    public class TokenData
    {
        public string? Matricula { get; set; }
        public string? NombreUsuario { get; set; }
        public string? AreaUsuario { get; set; }
        public string? DivisionUsuario { get; set; }
        public string? Perfil { get; set; }
        public string Cargo { get; set; } = string.Empty;
        public ExcepcionesFiltroUsuario UserExcepcionesFiltro { get; set; } = new ExcepcionesFiltroUsuario();
    }

    public class ExcepcionesFiltroUsuario
    {
        public List<ExcepcionesFiltroUsuarioTipo> TipoExcepcion { get; set; } = new List<ExcepcionesFiltroUsuarioTipo>();
    }

    public class ExcepcionesFiltroUsuarioTipo
    {
        public int IdAutonomia { get; set; }
        public int IdTipoExcepcion { get; set; }
        public string TipoExcepcionDes { get; set; } = string.Empty;
        public List<List<ExcepcionesFiltroUsuarioProducto>> ProductoExcepcion { get; set; } = new List<List<ExcepcionesFiltroUsuarioProducto>>();
    }
    public class ExcepcionesFiltroUsuarioProducto
    {
        public string DescripcionProducto { get; set; } = string.Empty;
        public string CodigoProducto { get; set; } = string.Empty;
        public decimal MenorIgual { get; set; } = 0M;
        public decimal Menor { get; set; } = 0M;
        public decimal MayorIgual { get; set; } = 0M;
        public decimal Mayor { get; set; } = 0M;
        public string Grupo { get; set; } = string.Empty;
    }
}
