
using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Repositories.Payments
{
    public interface IPaymentMethodRepo
    {
        Task<ICollection<PaymentMethodVM>> GetPaymentMethodsAsync();
        Task<PaymentMethodVM> GetPaymentMethodByIdAsync(int paymentMethodId);
        Task<ICollection<PaymentMethodVM>> GetPaymentMethodByUserIdAsync(string userId);
        Task<PaymentMethodVM> CreatePaymentMethodAsync(PaymentMethodVM paymentMethodVM);
        Task<PaymentMethodVM> UpdatePaymentMethodAsync(int paymentMethodId, PaymentMethodVM paymentMethodVM);
        Task<PaymentMethodVM> DeletePaymentMethodAsync(int paymentMethodId);
        Task<bool> IsPaymentMethodExistIdAsync(int paymentMethodId);
    }
}
