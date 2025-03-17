using MICRE_APP_EXCEPCIONES.Models;
using MICRE_APP_EXCEPCIONES.TDOs;
using NETCORE.Models.APIResponse;

namespace NETCORE.Models
{
    public class ExceptionViewModel
    {
        public List<Excepcion>? excepcionApi;
        public ExceptionModel? exception;
        public UpdateExcep? updateExcep;
        public List<ProductoInmuebleResponse>? ProductosChecks { get; set; }
        public List<ExceptionIdRequest>? Excepciones { get; set;}
        public List<ExceptionIdData>? exceptionList { get; set; }
    }
}
