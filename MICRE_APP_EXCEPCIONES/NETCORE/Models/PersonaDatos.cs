namespace NETCORE.Models
{
    public class PersonaDatos
    {
        public string? Ci { get; set; }
        public string? Extension { get; set; }
        public string? Complemento { get; set; }
        public string? Cic { get; set; }
        public string? Nombre { get; set; }
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public int IdEstadoCivil { get; set; }
        public bool TieneConyugue { get; set; }
        public string? NombreConyugue { get; set; }
        public string? PaternoConyugue { get; set; }
        public string? MaternoConyugue { get; set; }
        public int IdSituacionLaboralConyugue { get; set; }
        public int IdProfesionConyugue { get; set; }
        public int IdEstadoCivilConyugue { get; set; }
        public int IdActividadEconomicaConyugue { get; set; }
        public string? EmpresaConyugue { get; set; }
        public string? CiConyugue { get; set; }
        public string? ExtensionConyugue { get; set; }
        public string? ComplementoConyugue { get; set; }
        public int IdSituacionLaboral { get; set; }
        public int IdProfesion { get; set; }
        public int IdActividadEconomica { get; set; }
        public string? Empresa { get; set; }
        public int idAntiguedadLaboral { get; set; }
        public int idAntiguedadLaboralConyugue { get; set; }
        public bool ClientePDH { get; set; }
    }
}
