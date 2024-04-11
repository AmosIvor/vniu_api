using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Orders;
using vniu_api.Repositories;
using vniu_api.Repositories.Orders;
using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Services.Orders
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderLineRepo _orderLineRepo;

        public OrderRepo(DataContext context, IMapper mapper, IOrderLineRepo orderLineRepo)
        {
            _context = context;
            _mapper = mapper;
            _orderLineRepo = orderLineRepo;
        }

        public async Task<OrderVM> CreateOrderAsync(OrderVM orderVM)
        {
            // map
            var order = _mapper.Map<Order>(orderVM);

            // add database
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            // return result
            var newOrderVM = _mapper.Map<OrderVM>(order);

            return newOrderVM;
        }

        public async Task<OrderVM> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                // order not found
                throw new Exception("Order not found");
            }

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            // map
            var orderVM = _mapper.Map<OrderVM>(order);

            return orderVM;
        }

        public async Task<OrderVM> GetOrderByIdAsync(int orderId)
        {
            // check exist id
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            var orderVM = _mapper.Map<OrderVM>(order);

            return orderVM;
        }

        public async Task<ICollection<OrderVM>> GetOrdersAsync()
        {
            var orders = await _context.Orders.OrderByDescending(p => p.OrderCreateAt).ToListAsync();

            var ordersVM = _mapper.Map<ICollection<OrderVM>>(orders);

            return ordersVM;
        }

        public async Task<ICollection<OrderVM>> GetOrdersByOrderStatusIdAsync(int orderStatusId)
        {
            // check exist order status
            var isOrderStatusExist = await _context.OrderStatuses.AnyAsync(os => os.OrderStatusId == orderStatusId);

            if (isOrderStatusExist == false)
            {
                throw new Exception("Order status not found");
            }

            var orders = await _context.Orders.Where(o => o.OrderStatusId == orderStatusId).ToListAsync();

            var ordersVM = _mapper.Map<ICollection<OrderVM>>(orders);

            return ordersVM;
        }

        public async Task<ICollection<OrderVM>> GetOrdersByUserIdAndOrderStatusIdAsync(string userId, int orderStatusId)
        {
            // you don't need to check user id
            // check exist order status
            var isOrderStatusExist = await _context.OrderStatuses.AnyAsync(os => os.OrderStatusId == orderStatusId);

            if (isOrderStatusExist == false)
            {
                throw new Exception("Order status not found");
            }

            var orders = await _context.Orders.Where(o => o.UserId == userId && o.OrderStatusId == orderStatusId).ToListAsync();

            var ordersVM = _mapper.Map<ICollection<OrderVM>>(orders);

            return ordersVM;
        }

        public async Task<ICollection<OrderVM>> GetOrdersByUserIdAsync(string userId)
        {
            // you don't need to check user id
            var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();

            var ordersVM = _mapper.Map<ICollection<OrderVM>>(orders);

            return ordersVM;
        }

        public async Task<bool> IsOrderExistIdAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(p => p.OrderId == orderId);
        }

        public async Task<OrderVM> UpdateOrderStatusAsync(int orderId, int orderStatusId)
        {
            // check exist id
            var orderUpdate = await _context.Orders.FindAsync(orderStatusId);

            if (orderUpdate == null)
            {
                throw new Exception("Order not found");
            }

            // exist order
            orderUpdate.OrderStatusId = orderStatusId;

            _context.Orders.Update(orderUpdate);

            await _context.SaveChangesAsync();

            // map
            var updateOrderVM = _mapper.Map<OrderVM>(orderUpdate);

            return updateOrderVM;
        }
    }
}
