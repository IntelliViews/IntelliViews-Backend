using AutoMapper;
using IntelliViews.API.DTOs.Authentication;
using IntelliViews.API.DTOs.Feedback;
using IntelliViews.API.DTOs.Threads;
using IntelliViews.API.DTOs.User;
using IntelliViews.Data.DataModels;


namespace IntelliViews.API.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            // MappingProfile for the ApplicationUser:
            CreateMap<InRegisterDTO, ApplicationUser>();
            CreateMap<ApplicationUser, OutRegisterDTO>();
            CreateMap<InAuthDTO, ApplicationUser>();
           
            CreateMap<InUserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, OutUserDTO>();

            // MappingProfile for Threads and feeedback:
            CreateMap<ThreadUser, OutThreadsDTO >();
            CreateMap<InThreadDTO, ThreadUser> ();
            CreateMap<InFeedbackDTO, Feedback>();
            CreateMap<Feedback, OutFeedbackDTO >();
            

        }
    }
}
