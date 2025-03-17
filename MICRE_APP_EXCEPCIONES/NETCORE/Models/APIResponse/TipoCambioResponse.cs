namespace MICRE_APP_EXCEPCIONES.Models.APIResponse
{
    public class TipoCambioResponse
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public CambioData? Data { get; set; }

    }
}
