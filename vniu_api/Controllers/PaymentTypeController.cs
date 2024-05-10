using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Attributes;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Payments;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeRepo _paymentTypeRepo;
        private readonly IResponseCacheService _responseCacheService;

        public PaymentTypeController(IPaymentTypeRepo paymentTypeRepo, IResponseCacheService responseCacheService)
        {
            _paymentTypeRepo = paymentTypeRepo;
            _responseCacheService = responseCacheService;
        }

        [HttpGet("get-all")]
        [Cache(1000)]
        public async Task<IActionResult> GetPaymentTypes()
        {
            try
            {
                var result = await _paymentTypeRepo.GetPaymentTypesAsync();

                return Ok(new SuccessResponse<ICollection<PaymentTypeVM>>()
                {
                    Message = "Get list payment type successfully",
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

        [HttpGet("{paymentTypeId}")]
        public async Task<IActionResult> GetPaymentTypeById(int paymentTypeId)
        {
            try
            {
                var result = await _paymentTypeRepo.GetPaymentTypeByIdAsync(paymentTypeId);

                return Ok(new SuccessResponse<PaymentTypeVM>()
                {
                    Message = "Get payment type successfully",
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
        public async Task<IActionResult> CreatePaymentType(PaymentTypeVM paymentTypeVM)
        {
            // clear cache
            string pattern = $"{HttpContext.Request.Path}/get-all";
            await _responseCacheService.RemoveCacheResponseAsync(pattern);

            // add new
            var newPaymentType = await _paymentTypeRepo.CreatePaymentTypeAsync(paymentTypeVM);

            return Ok(new SuccessResponse<PaymentTypeVM>()
            {
                Message = "Create payment type successfully",
                Data = newPaymentType
            });
        }

        [HttpPut("{paymentTypeId}")]
        public async Task<IActionResult> UpdatePaymentType(int paymentTypeId, PaymentTypeVM paymentTypeVM)
        {
            try
            {
                var paymentTypeUpdate = await _paymentTypeRepo.UpdatePaymentTypeAsync(paymentTypeId, paymentTypeVM);

                return Ok(new SuccessResponse<PaymentTypeVM>()
                {
                    Message = "Update payment type successfully",
                    Data = paymentTypeUpdate
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

        [HttpDelete("{paymentTypeId}")]
        public async Task<IActionResult> DeletePaymentType(int paymentTypeId)
        {
            var paymentTypeDelete = await _paymentTypeRepo.DeletePaymentTypeAsync(paymentTypeId);

            return Ok(new SuccessResponse<PaymentTypeVM>()
            {
                Message = "Delete payment type successfully",
                Data = paymentTypeDelete
            });
        }
    }
}
