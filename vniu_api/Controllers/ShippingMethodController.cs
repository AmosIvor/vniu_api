using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Shippings;
using vniu_api.ViewModels.ShippingViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodRepo _shippingMethodRepo;

        public ShippingMethodController(IShippingMethodRepo shippingMethodRepo)
        {
            _shippingMethodRepo = shippingMethodRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetShippingMethods()
        {
            try
            {
                var result = await _shippingMethodRepo.GetShippingMethodsAsync();

                return Ok(new SuccessResponse<ICollection<ShippingMethodVM>>()
                {
                    Message = "Get list shipping method successfully",
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

        [HttpGet("{shippingMethodId}")]
        public async Task<IActionResult> GetShippingMethodById(int shippingMethodId)
        {
            try
            {
                var result = await _shippingMethodRepo.GetShippingMethodByIdAsync(shippingMethodId);

                return Ok(new SuccessResponse<ShippingMethodVM>()
                {
                    Message = "Get shipping method successfully",
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
        public async Task<IActionResult> CreateShippingMethod(ShippingMethodVM shippingMethodVM)
        {
            try
            {
                var newShippingMethod = await _shippingMethodRepo.CreateShippingMethodAsync(shippingMethodVM);

                return Ok(new SuccessResponse<ShippingMethodVM>()
                {
                    Message = "Create shipping method successfully",
                    Data = newShippingMethod
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

        [HttpPut("{shippingMethodId}")]
        public async Task<IActionResult> UpdateShippingMethod(int shippingMethodId, ShippingMethodVM shippingMethodVM)
        {
            try
            {
                var shippingMethodUpdate = await _shippingMethodRepo.UpdateShippingMethodAsync(shippingMethodId, shippingMethodVM);

                return Ok(new SuccessResponse<ShippingMethodVM>()
                {
                    Message = "Update shipping method successfully",
                    Data = shippingMethodUpdate
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

        [HttpDelete("{shippingMethodId}")]
        public async Task<IActionResult> DeleteShippingMethod(int shippingMethodId)
        {
            try
            {
                var shippingMethodDelete = await _shippingMethodRepo.DeleteShippingMethodAsync(shippingMethodId);

                return Ok(new SuccessResponse<ShippingMethodVM>()
                {
                    Message = "Delete shipping method successfully",
                    Data = shippingMethodDelete
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
