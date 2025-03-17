namespace NETCORE.TDOs
{
    public class InformacionLaboralConyugueTDO
    {
        public int idSituacionLaboral { get; set; }
        public int idAntiguedadLaboral { get; set; }
        public string? nombreEmpresa { get; set; }
        public int actividadEconomica { get; set; }
        public int actividadEconomica2 { get; set; }
        public int idProfesion { get; set; }
        public bool pagoHaberes { get; set; }
        public decimal ingresoLiquidoBOL { get; set; }
        public decimal otrosIngresosBOL { get; set; }
        public decimal ingresoLiquidoBOL2 { get; set; }
        public decimal ingresoLiquidoUSD { get; set; }
        public decimal otrosIngresosUSD { get; set; }
        public decimal ingresoLiquidoUSD2 { get; set; }
    }
}
