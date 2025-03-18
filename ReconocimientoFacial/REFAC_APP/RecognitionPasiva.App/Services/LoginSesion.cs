using BCP.CROSS.MODELS.Response;

namespace RecognitionPasiva.App.Services
{
    public class LoginSesion
    {
        public string UserAgent { get; set; }
        public string UserId { get; set; }
        public string CI { get; set; }
        public int ChannelId { get; set; }

        public bool VerificacionAnverso { get; set; }
        public bool VerificacionReverso { get; set; }
        public bool VerificacionVideo { get; set; }
        public List<ParameterDataResponse> parameters { get; set; }
    }
}
