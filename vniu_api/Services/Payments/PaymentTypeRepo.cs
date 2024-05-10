using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vniu_api.Exceptions;
using vniu_api.Models.EF.Payments;
using vniu_api.Repositories;
using vniu_api.Repositories.Payments;
using vniu_api.ViewModels.PaymentsViewModels;

namespace vniu_api.Services.Payments
{
    public class PaymentTypeRepo : IPaymentTypeRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentTypeRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaymentTypeVM> CreatePaymentTypeAsync(PaymentTypeVM paymentTypeVM)
        {
            // check exist value
            var isExistValue = await IsPaymentTypeExistValueAsync(paymentTypeVM.PaymentTypeValue);

            if (isExistValue == true)
            {
                throw new DuplicateValueException("Payment Type value exists");
            }

            var paymentType = _mapper.Map<PaymentType>(paymentTypeVM);

            _context.PaymentTypes.Add(paymentType);

            await _context.SaveChangesAsync();

            var newPaymentTypeVM = _mapper.Map<PaymentTypeVM>(paymentType);

            return newPaymentTypeVM;
        }

        public async Task<PaymentTypeVM> DeletePaymentTypeAsync(int paymentTypeId)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(paymentTypeId);

            if (paymentType == null)
            {
                throw new NotFoundException("Payment Type not found");
            }

            _context.PaymentTypes.Remove(paymentType);

            await _context.SaveChangesAsync();

            var paymentTypeVM = _mapper.Map<PaymentTypeVM>(paymentType);

            return paymentTypeVM;
        }

        public async Task<PaymentTypeVM> GetPaymentTypeByIdAsync(int paymentTypeId)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(paymentTypeId);

            if (paymentType == null)
            {
                throw new Exception("Payment Type not found");
            }

            var paymentTypeVM = _mapper.Map<PaymentTypeVM>(paymentType);

            return paymentTypeVM;
        }

        public async Task<PaymentTypeVM> GetPaymentTypeByValueAsync(string paymentTypeValue)
        {
            var paymentType = await _context.PaymentTypes.SingleOrDefaultAsync(p => p.PaymentTypeValue == paymentTypeValue);

            if (paymentType == null)
            {
                throw new Exception("Payment Type not found");
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

        public async Task<bool> IsPaymentTypeExistValueAsync(string paymentTypeValue)
        {
            return await _context.PaymentTypes.AnyAsync(p => p.PaymentTypeValue == paymentTypeValue);
        }

        public async Task<PaymentTypeVM> UpdatePaymentTypeAsync(int paymentTypeId, PaymentTypeVM paymentTypeVM)
        {
            if (paymentTypeVM.PaymentTypeId != paymentTypeId)
            {
                throw new Exception("Payment Type Id is diffrent");
            }

            var isExistPaymentType = await IsPaymentTypeExistIdAsync(paymentTypeId);

            if (isExistPaymentType == false)
            {
                throw new Exception("Payment Type not found");
            }

            // check value
            var isExistValue = await IsPaymentTypeExistValueAsync(paymentTypeVM.PaymentTypeValue);

            if (isExistValue == true)
            {
                throw new Exception("Payment Type value exists");
            }

            var updatePaymentType = _mapper.Map<PaymentType>(paymentTypeVM);

            _context.PaymentTypes.Update(updatePaymentType);

            await _context.SaveChangesAsync();

            var updatePaymentTypeVM = _mapper.Map<PaymentTypeVM>(updatePaymentType);

            return updatePaymentTypeVM;
        }
    }
}
