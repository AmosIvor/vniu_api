using AutoMapper;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.EF.Promotions;
using vniu_api.ViewModels;

namespace vniu_api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // profiles
            CreateMap<User, UserVM>().ReverseMap();

            // promotions
            CreateMap<Promotion, PromotionVM>().ReverseMap();
        }
    }
}
