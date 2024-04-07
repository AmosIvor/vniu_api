using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Repositories;
using vniu_api.Repositories.Payments;
using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Services.Payments
{
    public class PaymentMethodRepo : IPaymentMethodRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentMethodRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<PaymentTypeVM> CreatePaymentTypeAsync(PaymentTypeVM paymentTypeVM)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentTypeVM> DeletePaymentTypeAsync(int paymentTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentTypeVM> GetPaymentTypeByIdAsync(int paymentTypeId)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(paymentTypeId);

            if (paymentType == null)
            {
                throw new Exception("Payment Type Not Found");
            }

            var paymentTypeVM = _mapper.Map<PaymentTypeVM>(paymentType);

            return paymentTypeVM;
        }

        public async Task<PaymentTypeVM> GetPaymentTypeByValueAsync(string paymentTypeValue)
        {
            var paymentType = await _context.PaymentTypes.SingleOrDefaultAsync(p => p.PaymentTypeValue == paymentTypeValue);

            if (paymentType == null)
            {
                throw new Exception("Payment Type Not Found");
            }

            var paymentTypeVM = _mapper.Map<PaymentTypeVM>(paymentType);

            return paymentTypeVM;
        }

        public async Task<ICollection<PaymentTypeVM>> GetPaymentTypesAsync()
        {
            var paymentTypes = await _context.PaymentTypes.OrderBy(p => p.PaymentTypeId).ToListAsync();

            var paymentTypesVM = _mapper.Map<ICollection<PaymentTypeVM>>(paymentTypes);

            return paymentTypesVM;
        }

        public async Task<bool> IsPaymentTypeExistIdAsync(int paymentTypeId)
        {
            return await _context.PaymentTypes.AnyAsync(p => p.PaymentTypeId == paymentTypeId);
        }

        public Task<PaymentTypeVM> UpdatePaymentTypeAsync(int paymentTypeId, PaymentTypeVM paymentTypeVM)
        {
            throw new NotImplementedException();
        }
    }
}
