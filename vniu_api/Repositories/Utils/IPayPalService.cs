using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.RequestsViewModels;

namespace vniu_api.Repositories.Utils
{
    public interface IPayPalService
    {
        Task<string> CreatePaymentUrl(PaymentRequest model, HttpContext context);
        Task<PaymentMethodVM> PaymentExecuteAsync(int orderId, IQueryCollection collections);
    }
}
