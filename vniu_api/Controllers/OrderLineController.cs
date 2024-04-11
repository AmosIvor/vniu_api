using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Orders;
using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineRepo _orderLineRepo;

        public OrderLineController(IOrderLineRepo orderLineRepo)
        {
            _orderLineRepo = orderLineRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetOrderLines()
        {
            try
            {
                var result = await _orderLineRepo.GetOrderLinesAsync();

                return Ok(new SuccessResponse<ICollection<OrderLineVM>>()
                {
                    Message = "Get list order line successfully",
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

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetOrderLineById(int reviewId)
        {
            try
            {
                var result = await _orderLineRepo.GetOrderLineByIdAsync(reviewId);

                return Ok(new SuccessResponse<OrderLineVM>()
                {
                    Message = "Get order line successfully",
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

        [HttpGet("{orderId}/order-lines")]
        public async Task<IActionResult> GetOrderLinesByOrderId(int orderId)
        {
            try
            {
                var result = await _orderLineRepo.GetOrderLineByOrderIdAsync(orderId);

                return Ok(new SuccessResponse<ICollection<OrderLineVM>>()
                {
                    Message = "Get list order line of order successfully",
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
        public async Task<IActionResult> CreateOrderLine(OrderLineVM orderLineVM)
        {
            try
            {
                var newOrderLine = await _orderLineRepo.CreateOrderLineAsync(orderLineVM);

                return Ok(new SuccessResponse<OrderLineVM>()
                {
                    Message = "Create order line successfully",
                    Data = newOrderLine
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

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateOrderLine(int reviewId, OrderLineVM orderLineVM)
        {
            try
            {
                var reviewUpdate = await _orderLineRepo.UpdateOrderLineAsync(reviewId, orderLineVM);

                return Ok(new SuccessResponse<OrderLineVM>()
                {
                    Message = "Update order line successfully",
                    Data = reviewUpdate
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

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteOrderLine(int reviewId)
        {
            try
            {
                var reviewDelete = await _orderLineRepo.DeleteOrderLineAsync(reviewId);

                return Ok(new SuccessResponse<OrderLineVM>()
                {
                    Message = "Delete order line successfully",
                    Data = reviewDelete
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
