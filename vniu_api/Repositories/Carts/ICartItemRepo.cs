
using vniu_api.ViewModels.CartsViewModels;

namespace vniu_api.Repositories.Carts
{
    public interface ICartItemRepo
    {
        public Task<ICollection<CartItemVM>> GetCartItemsAsync();
        public Task<ICollection<CartItemVM>> GetCartItemsByUserIdAsync(string userId);
        public Task<CartItemVM> CreateCartItemAsync(CartItemVM cartItemVM);
        public Task<CartItemVM> UpdateCartItemAsync(int productItemId, CartItemVM cartItemVM);
        public Task<CartItemVM> DeleteCartItemOfUserAsync(string userId , int productItemId);
        public Task<string> DeleteCartItemsOfUserAsync(string userId);
        public Task<bool> IsCartItemExistIdAsync(int cartItemId);
        public Task<bool> IsCartItemExistProductIdAsync(string userId, int productItemId);
    }
}
