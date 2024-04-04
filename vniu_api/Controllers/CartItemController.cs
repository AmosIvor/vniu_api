using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Carts;
using vniu_api.ViewModels.CartsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepo _cartItemRepo;
        public CartItemController(ICartItemRepo cartItemRepo)
        {
            _cartItemRepo = cartItemRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetCartItems()
        {
            try
            {
                var result = await _cartItemRepo.GetCartItemsAsync();

                return Ok(new SuccessResponse<ICollection<CartItemVM>>()
                {
                    Message = "Get list cart items successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItemsByUserId(string userId)
        {
            try
            {
                var result = await _cartItemRepo.GetCartItemsByUserIdAsync(userId);

                return Ok(new SuccessResponse<ICollection<CartItemVM>>()
                {
                    Message = "Get list cart items of user successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CartItemVM cartItemVM)
        {
            try
            {
                var newCartItem = await _cartItemRepo.CreateCartItemAsync(cartItemVM);

                return Ok(new SuccessResponse<CartItemVM>()
                {
                    Message = "Create cart item successfully",
                    Data = newCartItem
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateCartItem(int productId, CartItemVM cartItemVM)
        {
            try
            {
                var cartItemUpdate = await _cartItemRepo.UpdateCartItemAsync(productId, cartItemVM);

                return Ok(new SuccessResponse<CartItemVM>()
                {
                    Message = "Update cart item successfully",
                    Data = cartItemUpdate
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> DeleteCartItemOfUser(string userId, int productId)
        {
            try
            {
                var cartItemDelete = await _cartItemRepo.DeleteCartItemOfUserAsync(userId, productId);

                return Ok(new SuccessResponse<CartItemVM>()
                {
                    Message = "Delete cart item of user successfully",
                    Data = cartItemDelete
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCartItemsOfUser(string userId)
        {
            try
            {
                var cartItemsDelete = await _cartItemRepo.DeleteCartItemsOfUserAsync(userId);

                return Ok(new SuccessResponse<string>()
                {
                    Message = "Delete cart item of user successfully",
                    Data = cartItemsDelete
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }
    }
}
