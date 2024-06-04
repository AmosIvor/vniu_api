using Microsoft.AspNetCore.Mvc;
using vniu_api.Repositories.Profiles;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.Models.Responses;
using System.Net;
using vniu_api.ViewModels.ResponsesViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressRepo _userAddressRepo;

        public UserAddressController(IUserAddressRepo userAddressRepo)
        {
            _userAddressRepo = userAddressRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetUserAddresses()
        {
            try
            {
                var result = await _userAddressRepo.GetUserAddressesAsync();
                return Ok(new SuccessResponse<ICollection<UserAddressVM>>()
                {
                    Message = "Get list of user addresses successfully",
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

        [HttpGet("{userId}/addresses")]
        public async Task<IActionResult> GetAddressesByUserId(string userId)
        {
            try
            {
                var result = await _userAddressRepo.GetAddressesByUserIdAsync(userId);
                return Ok(new SuccessResponse<ICollection<AddressResponseVM>>()
                {
                    Message = "Get list of addresses for user successfully",
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

        [HttpPost("{userId}/{addressId}")]
        public async Task<IActionResult> CreateUserAddress(string userId, int addressId, [FromBody] UserAddressVM userAddressVM)
        {
            try
            {
                var result = await _userAddressRepo.CreateUserAddressAsync(userId, addressId, userAddressVM.IsDefault);
                return Ok(new SuccessResponse<UserAddressVM>()
                {
                    Message = "User address created successfully",
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

        [HttpPut("{userId}/{addressId}")]
        public async Task<IActionResult> SetDefaultAddress(string userId, int addressId)
        {
            try
            {
                var result = await _userAddressRepo.SetDefaultAddressAsync(userId, addressId);
                return Ok(new SuccessResponse<UserAddressVM>()
                {
                    Message = "Default address set successfully",
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
    }
}
