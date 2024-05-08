using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Exceptions;
using vniu_api.Repositories;
using vniu_api.Repositories.Carts;
using vniu_api.ViewModels.CartsViewModels;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Services.Carts
{
    public class CartRepo : ICartRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CartRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartVM> GetCartByIdAsync(int cartId)
        {
            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.CartId == cartId);

            if (cart == null)
            {
                throw new Exception("Cart not found");
            }

            var cartVM = _mapper.Map<CartVM>(cart);

            return cartVM;
        }

        public async Task<CartVM> GetCartByUserIdAsync(string userId)
        {
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                throw new NotFoundException("User not found");
            }

            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new ArgumentNullException("Cart is null");
            }

            // Get User (ref) => using ref of entry
            var e = _context.Entry(cart);
            await e.Reference(c => c.User).LoadAsync();

            var cartVM = _mapper.Map<CartVM>(cart);

            var userVM = _mapper.Map<UserVM>(cart.User);
            cartVM.UserVM = userVM;

            return cartVM;
        }

        public async Task<ICollection<CartVM>> GetCartsAsync()
        {
            var carts = await _context.Carts.OrderBy(c => c.CartId).ToListAsync();

            var cartsVM = _mapper.Map<ICollection<CartVM>>(carts);

            return cartsVM;
        }

        public async Task<bool> IsCartExistIdAsync(int cartId)
        {
            return await _context.Carts.AnyAsync(c => c.CartId == cartId);
        }
    }
}
