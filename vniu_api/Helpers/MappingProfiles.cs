using AutoMapper;
using vniu_api.Models.EF;
using vniu_api.ViewModels;

namespace vniu_api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserVM>().ReverseMap();
        }
    }
}
