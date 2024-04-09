using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Repositories;
using vniu_api.Repositories.Carts;
using vniu_api.ViewModels.CartsViewModels;

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
                throw new Exception("User not found");
            }

            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            var cartVM = _mapper.Map<CartVM>(cart);

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
