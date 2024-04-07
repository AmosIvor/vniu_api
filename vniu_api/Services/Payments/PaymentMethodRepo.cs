using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Payments;
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

        public async Task<PaymentMethodVM> CreatePaymentMethodAsync(PaymentMethodVM paymentMethodVM)
        {
            // check user exist
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == paymentMethodVM.UserId);

            if (isUserExist == false)
            {
                throw new Exception("User not found. Please re-check user");
            }

            // check payment type exist
            var isPaymentTypeExist = await _context.PaymentTypes.AnyAsync(pt => pt.PaymentTypeId == paymentMethodVM.PaymentTypeId);

            if (isPaymentTypeExist == false)
            {
                throw new Exception("Payment type not found");
            }

            // map
            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodVM);

            // add database
            _context.PaymentMethods.Add(paymentMethod);

            await _context.SaveChangesAsync();

            // return result
            var newPaymentMethodVM = _mapper.Map<PaymentMethodVM>(paymentMethod);

            return newPaymentMethodVM;
        }

        public async Task<PaymentMethodVM> DeletePaymentMethodAsync(int paymentMethodId)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodId);

            if (paymentMethod == null)
            {
                // payment method not found
                throw new Exception("Payment Method not found");
            }

            _context.PaymentMethods.Remove(paymentMethod);

            await _context.SaveChangesAsync();

            // map
            var paymentMethodVM = _mapper.Map<PaymentMethodVM>(paymentMethod);

            return paymentMethodVM;
        }

        public async Task<PaymentMethodVM> GetPaymentMethodByIdAsync(int paymentMethodId)
        {
            // check exist id
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodId);

            if (paymentMethod == null)
            {
                throw new Exception("Payment not found");
            }

            var paymentMethodVM = _mapper.Map<PaymentMethodVM>(paymentMethod);

            return paymentMethodVM;
        }

        public async Task<ICollection<PaymentMethodVM>> GetPaymentMethodByUserIdAsync(string userId)
        {
            // check exist user id
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                // user not found
                throw new Exception("User not found");
            }

            var paymentMethods = await _context.PaymentMethods.Where(p => p.UserId == userId).ToListAsync();

            var paymentMethodsVM = _mapper.Map<ICollection<PaymentMethodVM>>(paymentMethods);

            return paymentMethodsVM;
        }

        public async Task<ICollection<PaymentMethodVM>> GetPaymentMethodsAsync()
        {
            var paymentMethods = await _context.PaymentMethods.OrderBy(p => p.PaymentMethodId).ToListAsync();

            var paymentMethodsVM = _mapper.Map<ICollection<PaymentMethodVM>>(paymentMethods);

            return paymentMethodsVM;
        }

        public async Task<bool> IsPaymentMethodExistIdAsync(int paymentMethodId)
        {
            return await _context.PaymentMethods.AnyAsync(p => p.PaymentMethodId == paymentMethodId);
        }

        public async Task<PaymentMethodVM> UpdatePaymentMethodAsync(int paymentMethodId, PaymentMethodVM paymentMethodVM)
        {
            // check id different or not ?
            if (paymentMethodVM.PaymentMethodId != paymentMethodId)
            {
                throw new Exception("Payment Method Id is different");
            }

            // check exist id
            var isIdExist = await IsPaymentMethodExistIdAsync(paymentMethodId);

            if (isIdExist == false)
            {
                throw new Exception("Payment Method not found");
            }

            // check user
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == paymentMethodVM.UserId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            // check payment type
            var isPaymentTypeExist = await _context.PaymentTypes.AnyAsync(pt => pt.PaymentTypeId == paymentMethodVM.PaymentTypeId);

            if (isPaymentTypeExist == false)
            {
                throw new Exception("Payment Type not found");
            }

            // map
            var updatePaymentMethod = _mapper.Map<PaymentMethod>(paymentMethodVM);

            _context.PaymentMethods.Update(updatePaymentMethod);

            await _context.SaveChangesAsync();

            // result
            var updatePaymentMethodVM = _mapper.Map<PaymentMethodVM>(updatePaymentMethod);

            return updatePaymentMethodVM;
        }
    }
}
