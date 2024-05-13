using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.RequestsViewModels;

namespace vniu_api.Repositories.Utils
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentRequest model, HttpContext context);
        Task<PaymentMethodVM> PaymentExecuteAsync(IQueryCollection collections);
    }
}
