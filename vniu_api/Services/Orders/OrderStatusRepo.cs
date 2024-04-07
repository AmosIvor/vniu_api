using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Orders;
using vniu_api.Repositories;
using vniu_api.Repositories.Orders;
using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Services.Orders
{
    public class OrderStatusRepo : IOrderStatusRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderStatusRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderStatusVM> CreateOrderStatusAsync(OrderStatusVM orderStatusVM)
        {
            // check exist value
            var isExistName = await IsOrderStatusExistNameAsync(orderStatusVM.OrderStatusName);

            if (isExistName == true)
            {
                throw new Exception("Order Status exists");
            }

            var orderStatus = _mapper.Map<OrderStatus>(orderStatusVM);

            _context.OrderStatuses.Add(orderStatus);

            await _context.SaveChangesAsync();

            var newOrderStatusVM = _mapper.Map<OrderStatusVM>(orderStatus);

            return newOrderStatusVM;
        }

        public async Task<OrderStatusVM> DeleteOrderStatusAsync(int orderStatusId)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(orderStatusId);

            if (orderStatus == null)
            {
                throw new Exception("Order Status not found");
            }

            _context.OrderStatuses.Remove(orderStatus);

            await _context.SaveChangesAsync();

            var orderStatusVM = _mapper.Map<OrderStatusVM>(orderStatus);

            return orderStatusVM;
        }

        public async Task<OrderStatusVM> GetOrderStatusByIdAsync(int orderStatusId)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(orderStatusId);

            if (orderStatus == null)
            {
                throw new Exception("Order Status not found");
            }

            var orderStatusVM = _mapper.Map<OrderStatusVM>(orderStatus);

            return orderStatusVM;
        }

        public async Task<OrderStatusVM> GetOrderStatusByNameAsync(string orderStatusName)
        {
            var orderStatus = await _context.OrderStatuses.SingleOrDefaultAsync(p => p.OrderStatusName == orderStatusName);

            if (orderStatus == null)
            {
                throw new Exception("Order Status not found");
            }

            var orderStatusVM = _mapper.Map<OrderStatusVM>(orderStatus);

            return orderStatusVM;
        }

        public async Task<ICollection<OrderStatusVM>> GetOrderStatusesAsync()
        {
            var orderStatuss = await _context.OrderStatuses.OrderBy(p => p.OrderStatusId).ToListAsync();

            var orderStatussVM = _mapper.Map<ICollection<OrderStatusVM>>(orderStatuss);

            return orderStatussVM;
        }

        public async Task<bool> IsOrderStatusExistIdAsync(int orderStatusId)
        {
            return await _context.OrderStatuses.AnyAsync(p => p.OrderStatusId == orderStatusId);
        }

        public async Task<bool> IsOrderStatusExistNameAsync(string orderStatusName)
        {
            return await _context.OrderStatuses.AnyAsync(p => p.OrderStatusName == orderStatusName);
        }

        public async Task<OrderStatusVM> UpdateOrderStatusAsync(int orderStatusId, OrderStatusVM orderStatusVM)
        {
            if (orderStatusVM.OrderStatusId != orderStatusId)
            {
                throw new Exception("Order Status Id is diffrent");
            }

            var isExistOrderStatus = await IsOrderStatusExistIdAsync(orderStatusId);

            if (isExistOrderStatus == false)
            {
                throw new Exception("Order Status not found");
            }

            // check value
            var isExistName = await IsOrderStatusExistNameAsync(orderStatusVM.OrderStatusName);

            if (isExistName == true)
            {
                throw new Exception("Order Status value exists");
            }

            var updateOrderStatus = _mapper.Map<OrderStatus>(orderStatusVM);

            _context.OrderStatuses.Update(updateOrderStatus);

            await _context.SaveChangesAsync();

            var updateOrderStatusVM = _mapper.Map<OrderStatusVM>(updateOrderStatus);

            return updateOrderStatusVM;
        }
    }
}
