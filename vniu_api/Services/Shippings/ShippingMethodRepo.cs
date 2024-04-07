using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Shippings;
using vniu_api.Repositories;
using vniu_api.Repositories.Shippings;
using vniu_api.ViewModels.ShippingViewModels;

namespace vniu_api.Services.Shippings
{
    public class ShippingMethodRepo : IShippingMethodRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShippingMethodRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShippingMethodVM> CreateShippingMethodAsync(ShippingMethodVM shippingMethodVM)
        {
            // check exist name
            var isExistName = await IsShippingMethodExistNameAsync(shippingMethodVM.ShippingMethodName);

            if (isExistName == true)
            {
                throw new Exception("Shipping Method exists");
            }

            var shippingMethod = _mapper.Map<ShippingMethod>(shippingMethodVM);

            _context.ShippingMethods.Add(shippingMethod);

            await _context.SaveChangesAsync();

            var newShippingMethodVM = _mapper.Map<ShippingMethodVM>(shippingMethod);

            return newShippingMethodVM;
        }

        public async Task<ShippingMethodVM> DeleteShippingMethodAsync(int shippingMethodId)
        {
            var shippingMethod = await _context.ShippingMethods.FindAsync(shippingMethodId);

            if (shippingMethod == null)
            {
                throw new Exception("Shipping Method not found");
            }

            _context.ShippingMethods.Remove(shippingMethod);

            await _context.SaveChangesAsync();

            var shippingMethodVM = _mapper.Map<ShippingMethodVM>(shippingMethod);

            return shippingMethodVM;
        }

        public async Task<ShippingMethodVM> GetShippingMethodByIdAsync(int shippingMethodId)
        {
            var shippingMethod = await _context.ShippingMethods.FindAsync(shippingMethodId);

            if (shippingMethod == null)
            {
                throw new Exception("Shipping Method not found");
            }

            var shippingMethodVM = _mapper.Map<ShippingMethodVM>(shippingMethod);

            return shippingMethodVM;
        }

        public async Task<ShippingMethodVM> GetShippingMethodByNameAsync(string shippingMethodName)
        {
            var shippingMethod = await _context.ShippingMethods.SingleOrDefaultAsync(p => p.ShippingMethodName == shippingMethodName);

            if (shippingMethod == null)
            {
                throw new Exception("Shipping Method not found");
            }

            var shippingMethodVM = _mapper.Map<ShippingMethodVM>(shippingMethod);

            return shippingMethodVM;
        }

        public async Task<ICollection<ShippingMethodVM>> GetShippingMethodsAsync()
        {
            var shippingMethods = await _context.ShippingMethods.OrderBy(p => p.ShippingMethodId).ToListAsync();

            var shippingMethodsVM = _mapper.Map<ICollection<ShippingMethodVM>>(shippingMethods);

            return shippingMethodsVM;
        }

        public async Task<bool> IsShippingMethodExistIdAsync(int shippingMethodId)
        {
            return await _context.ShippingMethods.AnyAsync(p => p.ShippingMethodId == shippingMethodId);
        }

        public async Task<bool> IsShippingMethodExistNameAsync(string shippingMethodName)
        {
            return await _context.ShippingMethods.AnyAsync(p => p.ShippingMethodName == shippingMethodName);
        }

        public async Task<ShippingMethodVM> UpdateShippingMethodAsync(int shippingMethodId, ShippingMethodVM shippingMethodVM)
        {
            if (shippingMethodVM.ShippingMethodId != shippingMethodId)
            {
                throw new Exception("Shipping Method Id is diffrent");
            }

            var isExistShippingMethod = await IsShippingMethodExistIdAsync(shippingMethodId);

            if (isExistShippingMethod == false)
            {
                throw new Exception("Shipping Method not found");
            }

            // check value
            var isExistName = await IsShippingMethodExistNameAsync(shippingMethodVM.ShippingMethodName);

            if (isExistName == true)
            {
                throw new Exception("Shipping Method name exists");
            }

            var updateShippingMethod = _mapper.Map<ShippingMethod>(shippingMethodVM);

            _context.ShippingMethods.Update(updateShippingMethod);

            await _context.SaveChangesAsync();

            var updateShippingMethodVM = _mapper.Map<ShippingMethodVM>(updateShippingMethod);

            return updateShippingMethodVM;
        }
    }
}
