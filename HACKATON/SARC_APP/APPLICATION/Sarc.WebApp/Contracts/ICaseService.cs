using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.Caso;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sarc.WebApp.Contracts
{
    public interface ICaseService
    {
        Task<ServiceResponse<CasoExpressResponse>> AddCasoExpressAsync(CasoExpressRequest casoExpressRequest);
        Task<ServiceResponse<CasoExpressResponse>> AddCasoAsync(CreateCasoDTO casoExpressRequest);
        Task<ServiceResponse<CasoDTOAll>> GetCasoByNroCasoAsync(string nroCaso);
        Task<ServiceResponse<List<CasoAnalista>>> GetCasosByAnalistaAsync(string matricula, string estado);
        Task<ServiceResponse<List<CasosByEstadoResponse>>> GetCasosByEstadoAsync(string estado);
        Task<ServiceResponse<bool?>> UpdateCasoOrigenAsync(UpdateOrigenCasoRequest request);
        Task<ServiceResponse<bool?>> UpdateDevolucionCobroAsync(UpdateDevolucionCobroRequest request);
        Task<ServiceResponse<bool?>> UpdateSolucionCasoAsync(UpdateCasoSolucionRequest request);
        Task<ServiceResponse<bool?>> UpdateSolucionInfoAdicionalAsync(UpdateSolucionCasoInfoAdicionalRequest request);
        Task<ServiceResponse<CasoAll>> GetCasoAllAsync(string nroCaso);
        Task<ServiceResponse<bool?>> CerrarCasoAsync(CerrarCasoRequest casoRequest);
        Task<ServiceResponse<bool?>> RechazarCasoSolucionadoAsync(UpdateCasoRechazarDTO request);
        Task<ServiceResponse<bool?>> RechazarCasoAsignadoAsync(UpdateCasoRechazoAnalistaDTO request);
        Task<ServiceResponse<bool?>> UpdateInfoRespuestaAsync(UpdateCasoViaEnvio request);
    }
}
