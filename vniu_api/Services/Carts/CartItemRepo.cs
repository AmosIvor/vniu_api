using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Carts;
using vniu_api.Repositories;
using vniu_api.Repositories.Carts;
using vniu_api.ViewModels.CartsViewModels;
using vniu_api.ViewModels.ProductsViewModels;

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

            // You don't need to check cart exist => because you register new account <=> you create new cart

            // mindset: if product exists => will increase quantity
            var cartItemExist = await _context.CartItems.SingleOrDefaultAsync(ci => ci.CartId == cartItemVM.CartId
                                                                           && ci.ProductItemId == cartItemVM.ProductItemId
                                                                           && ci.VariationId == cartItemVM.VariationId);

            if (cartItemExist != null)
            {
                // cart item exist => increase
                cartItemVM.Quantity = cartItemExist.Quantity + cartItemVM.Quantity;

                var cartItemUpdate = _mapper.Map<CartItem>(cartItemVM);

                _context.CartItems.Update(cartItemUpdate);

                await _context.SaveChangesAsync();

                var cartItemUpdateVM = _mapper.Map<CartItemVM>(cartItemUpdate);

                return cartItemUpdateVM;
            }

            // create new
            var newCartItem = _mapper.Map<CartItem>(cartItemVM);

            _context.CartItems.Add(newCartItem);

            await _context.SaveChangesAsync();

            var newCartItemVM = _mapper.Map<CartItemVM>(newCartItem);

            return newCartItemVM;
        }

        public async Task<CartItemVM> DeleteCartItemOfUserAsync(string userId, int productItemId)
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
            var cartItemDelete = await _context.CartItems.SingleOrDefaultAsync(c => c.CartId == cart!.CartId && 
                                                                                    c.ProductItemId == productItemId);

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
            // Check if user exists
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!isUserExist)
            {
                throw new Exception("User not found");
            }

            // Get the cart for the user
            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new Exception("Cart not found");
            }

            // Get the list of cart items and include product item and variation details
            var cartItems = await _context.CartItems
                .Where(c => c.CartId == cart.CartId)
                .Include(c => c.ProductItem)
                .ThenInclude(pi => pi.Product) // Include the parent Product entity
                .Include(c => c.ProductItem.ProductImages) // Include ProductImages
                .Include(c => c.ProductItem.Variations) // Include Variations
                .Include(c => c.ProductItem.Colour) // Include Colour
                .Include(c => c.Variation)
                .ThenInclude(v => v.Size) // Include Size details
                .ToListAsync();

            // Map cart items to CartItemVM including ProductItemVM and VariationVM
            var cartItemsVM = cartItems.Select(cartItem =>
            {
                var productItem = cartItem.ProductItem;
                var productItemVM = new ProductItemVM
                {
                    ProductItemId = productItem.ProductItemId,
                    ProductId = productItem.Product.ProductId, // Set ProductId from the parent Product
                    ProductName = productItem.Product.ProductName, // Set ProductName from the parent Product
                    ColourId = productItem.ColourId,
                    OriginalPrice = productItem.OriginalPrice,
                    SalePrice = productItem.SalePrice,
                    ProductItemSold = productItem.ProductItemSold,
                    ProductItemRating = productItem.ProductItemRating,
                    ProductItemCode = productItem.ProductItemCode,
                    ProductImage = productItem.ProductImages.FirstOrDefault() != null ? new ProductImageVM
                    {
                        ProductImageId = productItem.ProductImages.First().ProductImageId,
                        ProductImageUrl = productItem.ProductImages.First().ProductImageUrl,
                        ProductItemId = productItem.ProductImages.First().ProductItemId
                    } : null,
                    ProductImages = productItem.ProductImages.Select(image => new ProductImageVM
                    {
                        ProductImageId = image.ProductImageId,
                        ProductImageUrl = image.ProductImageUrl,
                        ProductItemId = image.ProductItemId
                    }).ToList(),
                    Variations = productItem.Variations.Select(v => new VariationVM
                    {
                        VariationId = v.VariationId,
                        QuantityInStock = v.QuantityInStock,
                        ProductItemId = v.ProductItemId,
                        SizeId = v.SizeId,
                        Size = new SizeOptionVM
                        {
                            SizeId = v.Size.SizeId,
                            SizeName = v.Size.SizeName,
                            SortOrder = v.Size.SortOrder
                        }
                    }).ToList(),
                    ColourVMs = productItem.Colour != null ? new List<ColourVM>
            {
                new ColourVM
                {
                    ColourId = productItem.Colour.ColourId,
                    ColourName = productItem.Colour.ColourName,
                    // Include other necessary properties
                }
            } : new List<ColourVM>()
                };

                var cartItemVM = new CartItemVM
                {
                    CartItemId = cartItem.CartItemId,
                    CartId = cartItem.CartId,
                    ProductItemId = cartItem.ProductItemId,
                    Quantity = cartItem.Quantity,
                    ProductItemVM = productItemVM, // Assign the created ProductItemVM
                    VariationVM = _mapper.Map<VariationVM>(cartItem.Variation) // Map VariationVM using AutoMapper
                };

                return cartItemVM;
            }).ToList();

            return cartItemsVM;
        }




        public async Task<bool> IsCartItemExistIdAsync(int cartItemId)
        {
            return await _context.CartItems.AnyAsync(c => c.CartItemId == cartItemId);
        }

        public async Task<bool> IsCartItemExistProductIdAsync(string userId, int productItemId)
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
            var isProductExist = await _context.CartItems.SingleOrDefaultAsync(ci => ci.CartId == cart!.CartId &&
                                                                                     ci.ProductItemId == productItemId);

            return isProductExist != null;
        }

        public async Task<CartItemVM> UpdateCartItemAsync(int productItemId, CartItemVM cartItemVM)
        {
            // check product exist
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(ci => ci.CartId == cartItemVM.CartId &&
                                                                        ci.ProductItemId == cartItemVM.ProductItemId);

            if (cartItem == null)
            {
                throw new Exception("Product not found");
            }

            // In this step, product exists => you need update
            var cartItemUpdate = _mapper.Map<CartItem>(cartItemVM);

            _context.CartItems.Update(cartItemUpdate);

            await _context.SaveChangesAsync();

            var cartItemUpdateVM = _mapper.Map<CartItemVM>(cartItemUpdate);

            return cartItemUpdateVM;
        }
    }
}
