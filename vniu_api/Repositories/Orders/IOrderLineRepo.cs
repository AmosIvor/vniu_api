using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Repositories.Orders
{
    public interface IOrderLineRepo
    {
        Task<ICollection<OrderLineVM>> GetOrderLinesAsync();
        Task<OrderLineVM> GetOrderLineByIdAsync(int orderLineId);
        Task<ICollection<OrderLineVM>> GetOrderLineByOrderIdAsync(int orderId);
        Task<OrderLineVM> CreateOrderLineAsync(OrderLineVM orderLineVM);
        Task<OrderLineVM> UpdateOrderLineAsync(int orderLineId, OrderLineVM orderLineVM);
        Task<OrderLineVM> DeleteOrderLineAsync(int orderLineId);
        Task<bool> IsOrderLineExistIdAsync(int orderLineId);
    }
}
