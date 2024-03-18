using AutoMapper;
using IntelliViews.API.DTOs.Authentication;
using IntelliViews.API.DTOs.User;
using IntelliViews.Data.DataModels;


namespace IntelliViews.API.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            // Mapping profile for the ApplicationUser:
            CreateMap<InRegisterDTO, ApplicationUser>();
            CreateMap<ApplicationUser, OutRegisterDTO>();
            CreateMap<InAuthDTO, ApplicationUser>();
           
            CreateMap<InUserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, OutUserDTO>();
            

        }
    }
}
