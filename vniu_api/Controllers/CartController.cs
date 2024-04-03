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
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cartRepo;
        public CartController(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetCarts()
        {
            try
            {
                var carts = await _cartRepo.GetCartsAsync();

                return Ok(new SuccessResponse<ICollection<CartVM>>()
                {
                    Message = "Get list carts successfully",
                    Data = carts
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

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            try
            {
                var cart = await _cartRepo.GetCartByIdAsync(cartId);

                return Ok(new SuccessResponse<CartVM>()
                {
                    Message = "Get cart successfully",
                    Data = cart
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
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            try
            {
                var cart = await _cartRepo.GetCartByUserIdAsync(userId);

                return Ok(new SuccessResponse<CartVM>()
                {
                    Message = "Get cart by id user successfully",
                    Data = cart
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
