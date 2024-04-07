
using vniu_api.ViewModels.OrdersViewModels;

namespace vniu_api.Repositories.Orders
{
    public interface IOrderStatusRepo
    {
        Task<ICollection<OrderStatusVM>> GetOrderStatusesAsync();
        Task<OrderStatusVM> GetOrderStatusByIdAsync(int orderStatusId);
        Task<OrderStatusVM> GetOrderStatusByNameAsync(string orderStatusName);
        Task<OrderStatusVM> CreateOrderStatusAsync(OrderStatusVM orderStatusVM);
        Task<OrderStatusVM> UpdateOrderStatusAsync(int orderStatusId, OrderStatusVM orderStatusVM);
        Task<OrderStatusVM> DeleteOrderStatusAsync(int orderStatusId);
        Task<bool> IsOrderStatusExistIdAsync(int orderStatusId);
        Task<bool> IsOrderStatusExistNameAsync(string orderStatusName);
    }
}
