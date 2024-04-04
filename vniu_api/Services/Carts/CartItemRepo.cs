using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Carts;
using vniu_api.Repositories;
using vniu_api.Repositories.Carts;
using vniu_api.ViewModels.CartsViewModels;

namespace vniu_api.Services.Carts
{
    public class CartItemRepo : ICartItemRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CartItemRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartItemVM> CreateCartItemAsync(CartItemVM cartItemVM)
        {
            // call api to get cart id in CartController: FE

            // mindset: if product exists => will increase quantity

            var cartItem = _mapper.Map<CartItem>(cartItemVM);

            _context.CartItems.Add(cartItem);

            await _context.SaveChangesAsync();

            var newCartItemVM = _mapper.Map<CartItemVM>(cartItem);

            return newCartItemVM;
        }

        public async Task<CartItemVM> DeleteCartItemOfUserAsync(string userId, int productId)
        {
            // check user exist
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            // get cart id from user id => certainly exist when user registers
            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            // filter cart item which has product id and cart id
            var cartItemDelete = await _context.CartItems.SingleOrDefaultAsync(c => c.CartId == cart!.CartId && false);
            // change false <=> condition of product

            if (cartItemDelete == null)
            {
                throw new Exception("Cart item not found");
            }

            _context.CartItems.Remove(cartItemDelete);

            await _context.SaveChangesAsync();

            var cartItemsDeleteVM = _mapper.Map<CartItemVM>(cartItemDelete);

            return cartItemsDeleteVM;
        }

        public async Task<string> DeleteCartItemsOfUserAsync(string userId)
        {
            // from userId => get cartId => get list cart items of this user
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            // get list cart item from cart id
            var cartItems = await _context.CartItems.Where(c => c.Cart.CartId == cart!.CartId).ToListAsync();

            if (cartItems.Count == 0)
            {
                throw new Exception("None item in your cart");
            }

            // remove this list
            foreach (CartItem item in cartItems)
            {
                _context.CartItems.Remove(item);
            }

            await _context.SaveChangesAsync();

            return "Delete all item in cart successfully";
        }

        public async Task<ICollection<CartItemVM>> GetCartItemsAsync()
        {
            var cartItems = await _context.CartItems.OrderBy(c => c.CartItemId).ToListAsync();

            var cartItemsVM = _mapper.Map<ICollection<CartItemVM>>(cartItems);

            return cartItemsVM;
        }

        public async Task<ICollection<CartItemVM>> GetCartItemsByUserIdAsync(string userId)
        {
            // from userId => get cartId => get list cart items of this user
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            // get list cart item from cart id
            var cartItems = await _context.CartItems.Where(c => c.Cart.CartId == cart!.CartId).ToListAsync();

            var cartItemsVM = _mapper.Map<ICollection<CartItemVM>>(cartItems);

            return cartItemsVM;
        }

        public async Task<bool> IsCartItemExistIdAsync(int cartItemId)
        {
            return await _context.CartItems.AnyAsync(c => c.CartItemId == cartItemId);
        }

        public async Task<bool> IsCartItemExistProductIdAsync(string userId, int productId)
        {
            // check user exist
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            // get cart => get list cart item and check product exist in cart item
            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            // check product exist in cart
            var isProductExist = false;

            return isProductExist;
        }

        public async Task<CartItemVM> UpdateCartItemAsync(int productId, CartItemVM cartItemVM)
        {
            // check product exist ...
            var isProductExist = await _context.CartItems.AnyAsync(ci => ci.CartId == cartItemVM.CartId && false);

            if (isProductExist == false)
            {
                throw new Exception("Product not found");
            }

            // In this step, product exists
            var cartItemUpdate = _mapper.Map<CartItem>(cartItemVM);

            _context.CartItems.Update(cartItemUpdate);

            await _context.SaveChangesAsync();

            var cartItemUpdateVM = _mapper.Map<CartItemVM>(cartItemUpdate);

            return cartItemUpdateVM;
        }
    }
}
