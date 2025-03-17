using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.Client;

namespace Sarc.WebApp.Models.Caso
{
    public class ProcessCasoViewModel
    {
        public GetClienteResponse Cliente { get; set; }
        public ServiceResponse<CasoExpressResponse> Response { get; set; }
        public string TipoRegistro { get; set; }    
    }
}
