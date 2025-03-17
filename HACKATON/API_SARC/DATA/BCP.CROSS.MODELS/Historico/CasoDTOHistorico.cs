namespace BCP.CROSS.MODELS.Historico
{
    public class CasoDTOHistorico: CasoDTOBaseServicio
    {
        public string EstadoDescripcion { get; set; }
        public string TipoServicioDescripcion { get; set; }
        public string TipoSolucionDescripcion { get; set; }
        public string TipoCartaDescripcion { get; set; }
        public string SWErrorReg { get; set; }
        public string Canal { get; set; }
    }
}
