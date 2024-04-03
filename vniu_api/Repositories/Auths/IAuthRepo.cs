using Microsoft.AspNetCore.Identity;
using vniu_api.Models.EF;
using vniu_api.Models.EF.Auths;
using vniu_api.Models.Responses;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Repositories.Auths
{
    public interface IAuthRepo
    {
        public Task<UserVM> SignUpAsync(UserRegister userRegister);

        public Task<AuthResponse> SignInAsync(UserLogin userLogin);
    }
}
