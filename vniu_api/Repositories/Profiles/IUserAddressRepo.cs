using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.ResponsesViewModels;

namespace vniu_api.Repositories.Profiles
{
    public interface IUserAddressRepo
    {
        Task<ICollection<UserAddressVM>> GetUserAddressesAsync();
        Task<ICollection<AddressResponseVM>> GetAddressesByUserIdAsync(string userId);
        Task<UserAddressVM> CreateUserAddressAsync(string userId, int addressId, bool isDefault);
        Task<UserAddressVM> SetDefaultAddressAsync(string userId, int addressId);
    }
}
