using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Repositories.Payments
{
    public interface IPaymentTypeRepo
    {
        Task<ICollection<PaymentTypeVM>> GetPaymentTypesAsync();
        Task<PaymentTypeVM> GetPaymentTypeByIdAsync(int paymentTypeId);
        Task<PaymentTypeVM> GetPaymentTypeByValueAsync(string paymentTypeValue);
        Task<PaymentTypeVM> CreatePaymentTypeAsync(PaymentTypeVM paymentTypeVM);
        Task<PaymentTypeVM> UpdatePaymentTypeAsync(int paymentTypeId, PaymentTypeVM paymentTypeVM);
        Task<PaymentTypeVM> DeletePaymentTypeAsync(int paymentTypeId);
        Task<bool> IsPaymentTypeExistIdAsync(int paymentTypeId);
        Task<bool> IsPaymentTypeExistValueAsync(string paymentTypeValue);
    }
}
