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
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusRepo _orderStatusRepo;

        public OrderStatusController(IOrderStatusRepo orderStatusRepo)
        {
            _orderStatusRepo = orderStatusRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetOrderStatuses()
        {
            try
            {
                var result = await _orderStatusRepo.GetOrderStatusesAsync();

                return Ok(new SuccessResponse<ICollection<OrderStatusVM>>()
                {
                    Message = "Get list order status successfully",
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

        [HttpGet("{orderStatusId}")]
        public async Task<IActionResult> GetOrderStatusById(int orderStatusId)
        {
            try
            {
                var result = await _orderStatusRepo.GetOrderStatusByIdAsync(orderStatusId);

                return Ok(new SuccessResponse<OrderStatusVM>()
                {
                    Message = "Get order status successfully",
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
        public async Task<IActionResult> CreateOrderStatus(OrderStatusVM orderStatusVM)
        {
            try
            {
                var newOrderStatus = await _orderStatusRepo.CreateOrderStatusAsync(orderStatusVM);

                return Ok(new SuccessResponse<OrderStatusVM>()
                {
                    Message = "Create order status successfully",
                    Data = newOrderStatus
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

        [HttpPut("{orderStatusId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderStatusId, OrderStatusVM orderStatusVM)
        {
            try
            {
                var orderStatusUpdate = await _orderStatusRepo.UpdateOrderStatusAsync(orderStatusId, orderStatusVM);

                return Ok(new SuccessResponse<OrderStatusVM>()
                {
                    Message = "Update order status successfully",
                    Data = orderStatusUpdate
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

        [HttpDelete("{orderStatusId}")]
        public async Task<IActionResult> DeleteOrderStatus(int orderStatusId)
        {
            try
            {
                var orderStatusDelete = await _orderStatusRepo.DeleteOrderStatusAsync(orderStatusId);

                return Ok(new SuccessResponse<OrderStatusVM>()
                {
                    Message = "Delete order status successfully",
                    Data = orderStatusDelete
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
