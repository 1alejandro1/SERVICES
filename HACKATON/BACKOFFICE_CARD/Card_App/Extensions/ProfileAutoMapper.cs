using AutoMapper;
using BCP.CROSS.MODELS.Generated;
using Card_App.Models.Generated;

namespace Card_App.Extensions
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
           
            CreateMap<GeneratedViewModel, GeneratedRequest>();                        
        }
    }
}
