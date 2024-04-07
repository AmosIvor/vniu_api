
using vniu_api.ViewModels.ShippingViewModels;

namespace vniu_api.Repositories.Shippings
{
    public interface IShippingMethodRepo
    {
        Task<ICollection<ShippingMethodVM>> GetShippingMethodsAsync();
        Task<ShippingMethodVM> GetShippingMethodByIdAsync(int shippingMethodId);
        Task<ShippingMethodVM> GetShippingMethodByNameAsync(string shippingMethodName);
        Task<ShippingMethodVM> CreateShippingMethodAsync(ShippingMethodVM shippingMethodVM);
        Task<ShippingMethodVM> UpdateShippingMethodAsync(int shippingMethodId, ShippingMethodVM shippingMethodVM);
        Task<ShippingMethodVM> DeleteShippingMethodAsync(int shippingMethodId);
        Task<bool> IsShippingMethodExistIdAsync(int shippingMethodId);
        Task<bool> IsShippingMethodExistNameAsync(string shippingMethodName);
    }
}
