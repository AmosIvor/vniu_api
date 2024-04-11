using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Repositories.Orders
{
    public interface IOrderRepo
    {
        Task<ICollection<OrderVM>> GetOrdersAsync();
        Task<OrderVM> GetOrderByIdAsync(int orderId);
        Task<ICollection<OrderVM>> GetOrdersByUserIdAsync(string userId);
        Task<ICollection<OrderVM>> GetOrdersByOrderStatusIdAsync(int orderStatusId);
        Task<ICollection<OrderVM>> GetOrdersByUserIdAndOrderStatusIdAsync(string userId, int orderStatusId);
        Task<OrderVM> CreateOrderAsync(OrderVM orderVM);
        Task<OrderVM> UpdateOrderStatusAsync(int orderId, int orderStatusId);
        Task<OrderVM> DeleteOrderAsync(int orderId);
        Task<bool> IsOrderExistIdAsync(int orderId);
    }
}
