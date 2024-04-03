
using vniu_api.ViewModels.CartsViewModels;

namespace vniu_api.Repositories.Carts
{
    public interface ICartRepo
    {
        public Task<ICollection<CartVM>> GetCartsAsync();
        public Task<CartVM> GetCartByIdAsync(int cartId);
        public Task<CartVM> GetCartByUserIdAsync(string userId);
        public Task<bool> IsCartExistIdAsync(int cartId);
    }
}
