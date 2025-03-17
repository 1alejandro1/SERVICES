using MICRE_APP_EXCEPCIONES.Models;
using MICRE_APP_EXCEPCIONES.Models.APIResponse;
using MICRE_APP_EXCEPCIONES.TDOs;
using NETCORE.Models;
using NETCORE.Models.APIResponse;
using NETCORE.TDOs;

namespace NETCORE.Services.Interfaces
{
    public interface IFormularioInmuebleService
    {
        Task<PersonaResponse> FindUser(ClienteIdc busqueda);
        Task<FormInmuebleResponse> SubmitTitularForm(FormTitularTDO busqueda);
        Task<ExceptionResponse> RegistrarException(RegisterExceptionTDO model);
        
        Task<ResponseModel> GetProductos(string combo);
        Task<List<Excepcion>> GetException();
        Task<ExceptionIdRequest> GetExcepcionesByID(string id);
        Task<BackUpsResponse> GetBackUps(string id);
        Task<TipoCambioResponse> GetCambio();

        Task<RemoveExceptionResponse> PostRemoveException(string id);
        Task<RemoveExceptionResponse> PostUpdateException(UpdateExceptionRequest id);
        Task<RegisterRespladoResponse> PostRegisterBackUp(RegisterRespladoRequest backup);
        Task<RegisterRespladoResponse> PostRegisterBackupExcepcion(DocumentoRespaldoExcepcionRequest backup);
    }
}
