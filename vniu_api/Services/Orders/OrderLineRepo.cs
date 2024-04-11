using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Orders;
using vniu_api.Repositories;
using vniu_api.Repositories.Orders;
using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Services.Orders
{
    public class OrderLineRepo : IOrderLineRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderLineRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderLineVM> CreateOrderLineAsync(OrderLineVM orderLineVM)
        {
            // check product item exist
            var isProductItemExist = await _context.ProductImages.AnyAsync(u => u.ProductItemId == orderLineVM.ProductItemId);

            if (isProductItemExist == false)
            {
                throw new Exception("Product item not found. Please re-check user");
            }

            // check order exist
            var isOrderExist = await _context.Orders.AnyAsync(pt => pt.OrderId == orderLineVM.OrderId);

            if (isOrderExist == false)
            {
                throw new Exception("Order not found");
            }

            // map
            var orderLine = _mapper.Map<OrderLine>(orderLineVM);

            // add database
            _context.OrderLines.Add(orderLine);

            await _context.SaveChangesAsync();

            // return result
            var newOrderLineVM = _mapper.Map<OrderLineVM>(orderLine);

            return newOrderLineVM;
        }

        public async Task<OrderLineVM> DeleteOrderLineAsync(int orderLineId)
        {
            var orderLine = await _context.OrderLines.FindAsync(orderLineId);

            if (orderLine == null)
            {
                // orderLine not found
                throw new Exception("OrderLine not found");
            }

            _context.OrderLines.Remove(orderLine);

            await _context.SaveChangesAsync();

            // map
            var orderLineVM = _mapper.Map<OrderLineVM>(orderLine);

            return orderLineVM;
        }

        public async Task<OrderLineVM> GetOrderLineByIdAsync(int orderLineId)
        {
            // check exist id
            var orderLine = await _context.OrderLines.FindAsync(orderLineId);

            if (orderLine == null)
            {
                throw new Exception("OrderLine not found");
            }

            var orderLineVM = _mapper.Map<OrderLineVM>(orderLine);

            return orderLineVM;
        }

        public async Task<ICollection<OrderLineVM>> GetOrderLineByOrderIdAsync(int orderId)
        {
            // check product item exist
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // get list order line by order id
            var orderLines = await _context.OrderLines.Where(ol => ol.OrderId == orderId).ToListAsync();

            var orderLinesVM = _mapper.Map<ICollection<OrderLineVM>>(orderLines);

            return orderLinesVM;
        }

        public async Task<ICollection<OrderLineVM>> GetOrderLinesAsync()
        {
            var orderLines = await _context.OrderLines.OrderByDescending(p => p.OrderLineId).ToListAsync();

            var orderLinesVM = _mapper.Map<ICollection<OrderLineVM>>(orderLines);

            return orderLinesVM;
        }

        public async Task<bool> IsOrderLineExistIdAsync(int orderLineId)
        {
            return await _context.OrderLines.AnyAsync(p => p.OrderLineId == orderLineId);
        }

        public async Task<OrderLineVM> UpdateOrderLineAsync(int orderLineId, OrderLineVM orderLineVM)
        {
            // check id different or not ?
            if (orderLineVM.OrderLineId != orderLineId)
            {
                throw new Exception("OrderLine Id is different");
            }

            // check exist id
            var isIdExist = await IsOrderLineExistIdAsync(orderLineId);

            if (isIdExist == false)
            {
                throw new Exception("OrderLine not found");
            }

            // check product item exist
            var isProductItemExist = await _context.ProductImages.AnyAsync(u => u.ProductItemId == orderLineVM.ProductItemId);

            if (isProductItemExist == false)
            {
                throw new Exception("Product item not found. Please re-check user");
            }

            // check order exist
            var isOrderExist = await _context.Orders.AnyAsync(pt => pt.OrderId == orderLineVM.OrderId);

            if (isOrderExist == false)
            {
                throw new Exception("Order not found");
            }

            // map
            var updateOrderLine = _mapper.Map<OrderLine>(orderLineVM);

            _context.OrderLines.Update(updateOrderLine);

            await _context.SaveChangesAsync();

            // result
            var updateOrderLineVM = _mapper.Map<OrderLineVM>(updateOrderLine);

            return updateOrderLineVM;
        }
    }
}
