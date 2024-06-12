using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Orders;
using vniu_api.Services.Orders;
using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        public OrderController(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var result = await _orderRepo.GetOrdersAsync();

                return Ok(new SuccessResponse<ICollection<OrderVM>>()
                {
                    Message = "Get list order successfully",
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

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var result = await _orderRepo.GetOrderByIdAsync(orderId);

                return Ok(new SuccessResponse<OrderVM>()
                {
                    Message = "Get order successfully",
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

        [HttpGet("orders/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            try
            {
                var result = await _orderRepo.GetOrdersByUserIdAsync(userId);

                return Ok(new SuccessResponse<ICollection<OrderVM>>()
                {
                    Message = "Get list order of user successfully",
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

        [HttpGet("{orderStatusId}/orders")]
        public async Task<IActionResult> GetOrdersByOrderStatusId(int orderStatusId)
        {
            try
            {
                var result = await _orderRepo.GetOrdersByOrderStatusIdAsync(orderStatusId);

                return Ok(new SuccessResponse<ICollection<OrderVM>>()
                {
                    Message = "Get list order of order status successfully",
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

        //[HttpGet("{userId}/orders/{orderStatusId}")]
        //public async Task<IActionResult> GetOrdersByUserIdAndOrderStatusId(string userId, int orderStatusId)
        //{
        //    try
        //    {
        //        var result = await _orderRepo.GetOrdersByUserIdAndOrderStatusIdAsync(userId, orderStatusId);

        //        return Ok(new SuccessResponse<ICollection<OrderVM>>()
        //        {
        //            Message = "Get list order of user successfully",
        //            Data = result
        //        });
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(new ErrorResponse()
        //        {
        //            Status = (int)HttpStatusCode.BadRequest,
        //            Title = e.Message
        //        });
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderVM orderVM, [FromQuery] int paymentType)
        {
            try
            {
                var newOrder = await _orderRepo.CreateOrderAsync(orderVM, paymentType);

                Console.WriteLine(newOrder);

                return Ok(new SuccessResponse<OrderVM>()
                {
                    Message = "Create order successfully",
                    Data = newOrder
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

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, int orderStatusId)
        {
            try
            {
                var orderUpdate = await _orderRepo.UpdateOrderStatusAsync(orderId, orderStatusId);

                return Ok(new SuccessResponse<OrderVM>()
                {
                    Message = "Update order successfully",
                    Data = orderUpdate
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

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var orderDelete = await _orderRepo.DeleteOrderAsync(orderId);

                return Ok(new SuccessResponse<OrderVM>()
                {
                    Message = "Delete order successfully",
                    Data = orderDelete
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
