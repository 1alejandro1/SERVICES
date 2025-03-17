using AutoMapper;
using BCP.CROSS.MODELS.Caso;
using BCP.CROSS.MODELS.WcfSwamp;
using Sarc.WebApp.Models.Analista;
using SARCAPP.APPLICATION.Sarc.WebApp.Models.Caso;

namespace Sarc.WebApp.Extensions
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<CreateCasoExpressViewModel, CasoExpressRequest>();
            CreateMap<CreateCasoViewModel, CreateCasoDTO>();
            CreateMap<TransactionRequest, CobroPR>();
            CreateMap<TransactionRequest, DevolucionPR>();
            CreateMap<CasoDTOAll, RechazarCasoViewModel>();
            CreateMap<RechazarCasoViewModel, UpdateCasoRechazoAnalistaDTO>();
        }
    }
}
