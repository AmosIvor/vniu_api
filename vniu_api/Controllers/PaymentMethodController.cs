using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Payments;
using vniu_api.Services.Payments;
using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodRepo _paymentMethodRepo;

        public PaymentMethodController(IPaymentMethodRepo paymentMethodRepo)
        {
            _paymentMethodRepo = paymentMethodRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetPaymentMethods()
        {
            try
            {
                var result = await _paymentMethodRepo.GetPaymentMethodsAsync();

                return Ok(new SuccessResponse<ICollection<PaymentMethodVM>>()
                {
                    Message = "Get list payment method successfully",
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
        public async Task<IActionResult> CreatePaymentMethod(PaymentMethodVM paymentMethodVM)
        {
            try
            {
                var newPaymentMethod = await _paymentMethodRepo.CreatePaymentMethodAsync(paymentMethodVM);

                return Ok(new SuccessResponse<PaymentMethodVM>()
                {
                    Message = "Create payment method successfully",
                    Data = newPaymentMethod
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

        [HttpPut("{paymentMethodId}")]
        public async Task<IActionResult> UpdatePaymentMethod(int paymentMethodId, PaymentMethodVM paymentMethodVM)
        {
            try
            {
                var paymentMethodUpdate = await _paymentMethodRepo.UpdatePaymentMethodAsync(paymentMethodId, paymentMethodVM);

                return Ok(new SuccessResponse<PaymentMethodVM>()
                {
                    Message = "Update payment method successfully",
                    Data = paymentMethodUpdate
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

        [HttpDelete("{paymentMethodId}")]
        public async Task<IActionResult> DeletePaymentMethod(int paymentMethodId)
        {
            try
            {
                var paymentMethodDelete = await _paymentMethodRepo.DeletePaymentMethodAsync(paymentMethodId);

                return Ok(new SuccessResponse<PaymentMethodVM>()
                {
                    Message = "Delete payment method successfully",
                    Data = paymentMethodDelete
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
