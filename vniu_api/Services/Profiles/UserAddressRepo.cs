using AutoMapper;
using vniu_api.Repositories.Profiles;
using vniu_api.Repositories;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.ResponsesViewModels;
using vniu_api.Models.EF.Profiles;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
namespace vniu_api.Services.Profiles
{
    public class UserAddressRepo : IUserAddressRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserAddressRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserAddressVM> CreateUserAddressAsync(string userId, int addressId, bool isDefault)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found");

            var address = await _context.Addresses.FindAsync(addressId);
            if (address == null) throw new Exception("Address not found");

            var userAddress = new UserAddress
            {
                UserId = userId,
                AddressId = addressId,
                IsDefault = isDefault
            };

            _context.UserAddresses.Add(userAddress);

            if (isDefault)
            {
                var currentDefaultAddresses = _context.UserAddresses
                    .Where(ua => ua.UserId == userId && ua.IsDefault)
                    .ToList();

                foreach (var currentDefaultAddress in currentDefaultAddresses)
                {
                    currentDefaultAddress.IsDefault = false;
                }
            }

            await _context.SaveChangesAsync();

            var userAddressVM = _mapper.Map<UserAddressVM>(userAddress);
            return userAddressVM;
        }

        public async Task<ICollection<UserAddressVM>> GetUserAddressesAsync()
        {
            var userAddressesVM = await _context.UserAddresses.ProjectTo<UserAddressVM>(_mapper.ConfigurationProvider).ToListAsync();
            return userAddressesVM;

        }

        public async Task<ICollection<AddressResponseVM>> GetAddressesByUserIdAsync(string userId)
        {
            var addressesVM = (from userAddress in _context.UserAddresses
                               join address in _context.Addresses on userAddress.AddressId equals address.AddressId
                               join user in _context.Users on userAddress.UserId equals user.Id
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
                                   IsDefault = userAddress.IsDefault,
                                   User = new UserVM
                                   {
                                       Id = user.Id,
                                       UserName = user.UserName,
                                       Email = user.Email,
                                       PhoneNumber = user.PhoneNumber,
                                       Male = user.Male,
                                       DateBirth = user.DateBirth,
                                       Avatar = user.Avatar
                                   }
                               }).ToList();

            return addressesVM;

        }

        public async Task<UserAddressVM> SetDefaultAddressAsync(string userId, int addressId)
        {
            var userAddresses = _context.UserAddresses
                .Where(ua => ua.UserId == userId)
                .ToList();

            foreach (var userAddress in userAddresses)
            {
                userAddress.IsDefault = userAddress.AddressId == addressId;
            }

            await _context.SaveChangesAsync();

            var defaultAddress = userAddresses.FirstOrDefault(ua => ua.IsDefault);
            if (defaultAddress == null) throw new Exception("Default address not found");

            var userAddressVM = _mapper.Map<UserAddressVM>(defaultAddress);
            return userAddressVM;
        }
    }
}
