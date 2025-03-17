namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class CasoDTOBaseAtencion: CasoDTOBase
    {
        public string Idc { get; set; }
        public string ClienteIdc { get; set; }
        public string ClienteTipo { get; set; }
        public string ClienteExtension { get; set; }
        public string SWErrorSolucion { get; set; }
        public string SWErrorReg { get; set; }
        public int DiasAtencion { get; set; }
    }
}
