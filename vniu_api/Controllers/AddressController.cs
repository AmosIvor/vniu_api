using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Net;
using System.Xml;
using vniu_api.Models.Responses;
using vniu_api.Repositories;
using vniu_api.Repositories.Profiles;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.ResponsesViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepo _addressRepo;
        private readonly DataContext _context;
        private readonly IElasticClient _elasticClient;

        public AddressController(IAddressRepo addressRepo, DataContext context, IElasticClient elasticClient)
        {
            _addressRepo = addressRepo;
            _context = context;
            _elasticClient = elasticClient;
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

                return Ok(new SuccessResponse<ICollection<AddressResponseVM>>()
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

        [HttpGet("elastic_search/retrieve_data")]
        public async Task<IActionResult> IndexData()
        {
            var dataConvert = await _context.Addresses.ToListAsync();

            var bulkAllObservable = _elasticClient.BulkAll(dataConvert, b => b.Index("address")
                                                                    .BackOffTime("30s")
                                                                    .RefreshOnCompleted());

            bulkAllObservable.Wait(TimeSpan.FromMinutes(15), response => { });
            return Ok("success");
        }
    }
}
