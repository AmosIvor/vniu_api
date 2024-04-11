
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.ResponsesViewModels;

namespace vniu_api.Repositories.Profiles
{
    public interface IAddressRepo
    {
        Task<ICollection<AddressVM>> GetAddressesAsync();
        Task<AddressVM> GetAddressByIdAsync(int addressId);
        Task<ICollection<AddressResponseVM>> GetAddressesByUserIdAsync(string userId);
        Task<AddressVM> CreateAddressAsync(string userId, AddressVM addressVM);
        Task<AddressVM> UpdateAddressAsync(int addressId, AddressVM addressVM);
        Task<AddressVM> DeleteAddressAsync(int addressId);
        Task<bool> IsAddressExistIdAsync(int addressId);
        Task<bool> IsUserHasAnyAddressAsync(string userId);
    }
}
