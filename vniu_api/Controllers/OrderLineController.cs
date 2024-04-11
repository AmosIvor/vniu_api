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

        [HttpGet("{orderLineId}")]
        public async Task<IActionResult> GetOrderLineById(int orderLineId)
        {
            try
            {
                var result = await _orderLineRepo.GetOrderLineByIdAsync(orderLineId);

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

        [HttpPut("{orderLineId}")]
        public async Task<IActionResult> UpdateOrderLine(int orderLineId, OrderLineVM orderLineVM)
        {
            try
            {
                var orderLineUpdate = await _orderLineRepo.UpdateOrderLineAsync(orderLineId, orderLineVM);

                return Ok(new SuccessResponse<OrderLineVM>()
                {
                    Message = "Update order line successfully",
                    Data = orderLineUpdate
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

        [HttpDelete("{orderLineId}")]
        public async Task<IActionResult> DeleteOrderLine(int orderLineId)
        {
            try
            {
                var orderLineDelete = await _orderLineRepo.DeleteOrderLineAsync(orderLineId);

                return Ok(new SuccessResponse<OrderLineVM>()
                {
                    Message = "Delete order line successfully",
                    Data = orderLineDelete
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
