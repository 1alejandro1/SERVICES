using BCP.CROSS.MODELS;

namespace Sarc.WebApp.Models.Analista
{
    public class ProcessUpdateSolucionCasoViewModel
    {
        public string NroCaso { get; set; }
        public string TipoCaso { get; set; }
        public ServiceResponse<bool?> SolucionCaso { get; set; }
        public ServiceResponse<bool?> UpdateRespuesta { get; set; }
        public ServiceResponse<bool?> CasoOrigen { get; set; }
        public ServiceResponse<bool?> InformacionAdicional { get; set; }
        public ServiceResponse<bool?> UpdateDevolucionCobro { get; set; }
        public ServiceResponse<bool?> UploadFilesSharepoint { get; set; }
    }
}
