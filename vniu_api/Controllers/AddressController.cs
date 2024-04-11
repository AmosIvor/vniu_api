using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Profiles;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepo _addressRepo;

        public AddressController(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                var result = await _addressRepo.GetAddressesAsync();

                return Ok(new SuccessResponse<ICollection<AddressVM>>()
                {
                    Message = "Get list address successfully",
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

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            try
            {
                var result = await _addressRepo.GetAddressByIdAsync(addressId);

                return Ok(new SuccessResponse<AddressVM>()
                {
                    Message = "Get address successfully",
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
        public async Task<IActionResult> GetAddressByUserId(string userId)
        {
            try
            {
                var result = await _addressRepo.GetAddressesByUserIdAsync(userId);

                return Ok(new SuccessResponse<ICollection<AddressVM>>()
                {
                    Message = "Get list address of user successfully",
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

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateAddress(string userId, AddressVM addressVM)
        {
            try
            {
                var newAddress = await _addressRepo.CreateAddressAsync(userId, addressVM);

                return Ok(new SuccessResponse<AddressVM>()
                {
                    Message = "Create address successfully",
                    Data = newAddress
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

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, AddressVM addressVM)
        {
            try
            {
                var addressUpdate = await _addressRepo.UpdateAddressAsync(addressId, addressVM);

                return Ok(new SuccessResponse<AddressVM>()
                {
                    Message = "Update address successfully",
                    Data = addressUpdate
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

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            try
            {
                var addressDelete = await _addressRepo.DeleteAddressAsync(addressId);

                return Ok(new SuccessResponse<AddressVM>()
                {
                    Message = "Delete address successfully",
                    Data = addressDelete
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
