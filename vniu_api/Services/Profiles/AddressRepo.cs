using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Profiles;
using vniu_api.Repositories;
using vniu_api.Repositories.Profiles;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.ResponsesViewModels;

namespace vniu_api.Services.Profiles
{
    public class AddressRepo : IAddressRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AddressRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AddressVM> CreateAddressAsync(string userId, AddressVM addressVM)
        {
            // get user
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var address = _mapper.Map<Address>(addressVM);

            _context.Addresses.Add(address);

            // check user have address or not ?
            var isUserHasAnyAddress = await IsUserHasAnyAddressAsync(userId);


            // add user_address
            var user_address = new UserAddress()
            {
                User = user,
                Address = address,
                IsDefault = !isUserHasAnyAddress
            };

            _context.UserAddresses.Add(user_address);

            await _context.SaveChangesAsync();

            var newAddressVM = _mapper.Map<AddressVM>(address);

            return newAddressVM;
        }

        public async Task<AddressVM> DeleteAddressAsync(int addressId)
        {
            var address = await _context.Addresses.FindAsync(addressId);

            if (address == null)
            {
                throw new Exception("Address not found");
            }

            _context.Addresses.Remove(address);

            await _context.SaveChangesAsync();

            var addressVM = _mapper.Map<AddressVM>(address);

            return addressVM;
        }

        public async Task<AddressVM> GetAddressByIdAsync(int addressId)
        {
            var address = await _context.Addresses.FindAsync(addressId);

            if (address == null)
            {
                throw new Exception("Address not found");
            }

            var addressVM = _mapper.Map<AddressVM>(address);

            return addressVM;
        }

        public async Task<ICollection<AddressVM>> GetAddressesAsync()
        {
            var addresss = await _context.Addresses.OrderBy(p => p.AddressId).ToListAsync();

            var addresssVM = _mapper.Map<ICollection<AddressVM>>(addresss);

            return addresssVM;
        }

        public async Task<ICollection<AddressResponseVM>> GetAddressesByUserIdAsync(string userId)
        {
            // check user exist
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);

            if (isUserExist == false)
            {
                throw new Exception("User not found");
            }

            // get address
            var addressesVM = await (from userAddress in _context.UserAddresses
                                   join address in _context.Addresses on userAddress.AddressId equals address.AddressId
                                   where userAddress.UserId == userId
                                   select new AddressResponseVM
                                   {
                                       AddressId = address.AddressId,
                                       UnitNumber = address.UnitNumber,
                                       StreetNumber = address.StreetNumber,
                                       AddressLine1 = address.AddressLine1,
                                       AddressLine2 = address.AddressLine2,
                                       District = address.District,
                                       City = address.City,
                                       IsDefault = userAddress.IsDefault
                                   }).ToListAsync();

            return addressesVM;
        }

        public async Task<bool> IsAddressExistIdAsync(int addressId)
        {
            return await _context.Addresses.AnyAsync(p => p.AddressId == addressId);
        }

        public async Task<bool> IsUserHasAnyAddressAsync(string userId)
        {
            var quantityAddress = await _context.UserAddresses.CountAsync(ua => ua.UserId == userId);

            return quantityAddress > 0;
        }

        public async Task<AddressVM> UpdateAddressAsync(int addressId, AddressVM addressVM)
        {
            if (addressVM.AddressId != addressId)
            {
                throw new Exception("Address Id is diffrent");
            }

            var isExistAddress = await IsAddressExistIdAsync(addressId);

            if (isExistAddress == false)
            {
                throw new Exception("Address not found");
            }

            var updateAddress = _mapper.Map<Address>(addressVM);

            _context.Addresses.Update(updateAddress);

            await _context.SaveChangesAsync();

            var updateAddressVM = _mapper.Map<AddressVM>(updateAddress);

            return updateAddressVM;
        }
    }
}
