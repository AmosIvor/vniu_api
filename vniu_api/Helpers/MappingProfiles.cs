using AutoMapper;
using vniu_api.Models;
using vniu_api.ViewModels;

namespace vniu_api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerVM>().ReverseMap();
        }
    }
}
