using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Lexico;
using BCP.CROSS.MODELS.SmartLink;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sarc.WebApp.Contracts
{
    public interface ILexicoService
    {
        Task<IEnumerable<Area>> GetAreaAllAsync();
        Task<IEnumerable<Atm>> GetAtmsAsync();
        Task<IEnumerable<Carta>> GetCartaAllAsync();
        Task<IEnumerable<ParametrosResponse>> GetCentrosCostoAsync();
        Task<IEnumerable<Ciudad>> GetCidudadesAsync(string departamentoId);
        Task<IEnumerable<DevolucionProductoServicioResponse>> GetCobServicioCanalCuentaByProductoIdServivioIdAsync(ServicioCanalCuentaRequest request);
        Task<IEnumerable<DevolucionProductoServicioResponse>> GetDevServicioCanalCuentaByProductoIdServivioIdAsync(ServicioCanalCuentaRequest request);
        Task<IEnumerable<ProductoServicioResponse>> GetDocumentacionRequerida(ProductoServicioRequest request);
        Task<ServiceResponse<int>> GetLimiteDevolucionAsync(string valor, string lexico = "");
        Task<IEnumerable<ParametroSarcResponse>> GetParametroSarcByTipoAsync(string lexico);
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<IEnumerable<ParametrosError>> GetSarcErrors();
        Task<IEnumerable<ServicioByProductoTipoEstadoResponse>> GetServiciosAsync(ServicioByProductoTipoEstadoRequest request);
        Task<IEnumerable<Sucursals>> GetSucursalesAsync();
        Task<ServiceResponse<TipoCambio>> GetTipoCambioAsync();
        Task<IEnumerable<TipoIdc>> GetTipoIdc();
        Task<IEnumerable<TipoSolucionResponse>> GetTiposSolucionAsync();
        Task<IEnumerable<ViasNotificacion>> GetViasNotificacionAsync();
        Task<IEnumerable<ValidaCuentaResponse>> ValidateCuentaAsync(string idc, string idcTipo, string idcExtensio, string nroCuenta);
    }
}