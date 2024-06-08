using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Payments;
using vniu_api.Repositories;
using vniu_api.Repositories.Orders;
using vniu_api.ViewModels.OrdersViewModels;
using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.ProductsViewModels;

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

        public async Task<OrderVM> CreateOrderAsync(OrderVM orderVM, int paymentType)
        {
            // create default payment method 
            var paymentMethod = new PaymentMethod()
            {
                PaymentTypeId = paymentType,
                PaymentStatus = 0 // default is 0 - unpaid => 1 - paid => 2 - error
            };

            _context.PaymentMethods.Add(paymentMethod);

            // map default payment to get paymentMethodId
            var paymentMethodVM = _mapper.Map<PaymentMethodVM>(paymentMethod);

            orderVM.PaymentMethodId = paymentMethodVM.PaymentMethodId;

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
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.ProductItem)
                        .ThenInclude(pi => pi.Product) // Include the parent Product entity
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.ProductItem.ProductImages) // Include ProductImages
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.ProductItem.Variations) // Include Variations
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.ProductItem.Colour) // Include Colour
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.Variation)
                        .ThenInclude(v => v.Size) // Include Size details
                .ToListAsync();

            var ordersVM = orders.Select(order =>
            {
                var orderLinesVM = order.OrderLines.Select(orderLine =>
                {
                    var productItem = orderLine.ProductItem;
                    var productItemVM = new ProductItemVM
                    {
                        ProductItemId = productItem.ProductItemId,
                        ProductId = productItem.Product.ProductId, // Set ProductId from the parent Product
                        ProductName = productItem.Product.ProductName, // Set ProductName from the parent Product
                        ColourId = productItem.ColourId,
                        OriginalPrice = productItem.OriginalPrice,
                        SalePrice = productItem.SalePrice,
                        ProductItemSold = productItem.ProductItemSold,
                        ProductItemRating = productItem.ProductItemRating,
                        ProductItemCode = productItem.ProductItemCode,
                        ProductImage = productItem.ProductImages.FirstOrDefault() != null ? new ProductImageVM
                        {
                            ProductImageId = productItem.ProductImages.First().ProductImageId,
                            ProductImageUrl = productItem.ProductImages.First().ProductImageUrl,
                            ProductItemId = productItem.ProductImages.First().ProductItemId
                        } : null,
                        ProductImages = productItem.ProductImages.Select(image => new ProductImageVM
                        {
                            ProductImageId = image.ProductImageId,
                            ProductImageUrl = image.ProductImageUrl,
                            ProductItemId = image.ProductItemId
                        }).ToList(),
                        Variations = productItem.Variations.Select(v => new VariationVM
                        {
                            VariationId = v.VariationId,
                            QuantityInStock = v.QuantityInStock,
                            ProductItemId = v.ProductItemId,
                            SizeId = v.SizeId,
                            Size = new SizeOptionVM
                            {
                                SizeId = v.Size.SizeId,
                                SizeName = v.Size.SizeName,
                                SortOrder = v.Size.SortOrder
                            }
                        }).ToList(),
                        ColourVMs = productItem.Colour != null ? new List<ColourVM>
                {
                    new ColourVM
                    {
                        ColourId = productItem.Colour.ColourId,
                        ColourName = productItem.Colour.ColourName,
                        // Include other necessary properties
                    }
                } : new List<ColourVM>()
                    };

                    var orderLineVM = new OrderLineVM
                    {
                        OrderLineId = orderLine.OrderLineId,
                        Quantity = orderLine.Quantity,
                        Price = orderLine.Price,
                        OrderId = orderLine.OrderId,
                        ProductItemId = orderLine.ProductItemId,
                        VariationId = orderLine.VariationId,
                        ProductItem = productItemVM, // Assign the created ProductItemVM
                        Variation = _mapper.Map<VariationVM>(orderLine.Variation) // Map VariationVM using AutoMapper
                    };

                    return orderLineVM;
                }).ToList();

                var orderVM = new OrderVM
                {
                    OrderId = order.OrderId,
                    OrderCreateAt = order.OrderCreateAt,
                    OrderUpdateAt = order.OrderUpdateAt,
                    OrderTotal = order.OrderTotal,
                    OrderNote = order.OrderNote,
                    OrderStatusId = order.OrderStatusId,
                    ShippingMethodId = order.ShippingMethodId,
                    PromotionId = order.PromotionId,
                    Address = order.Address,
                    Username = order.Username,
                    NumberPhone = order.NumberPhone,
                    PaymentMethodId = order.PaymentMethodId,
                    UserId = order.UserId,
                    OrderLines = orderLinesVM
                };

                return orderVM;
            }).ToList();

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
