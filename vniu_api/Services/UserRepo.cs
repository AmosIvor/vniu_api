using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Repositories;
using vniu_api.ViewModels;
using System;

namespace vniu_api.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<UserVM> CreateUserAsync(UserVM user)
        {
            throw new NotImplementedException();
        }

        public Task<UserVM> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserVM>> GetUsersAsync()
        {
            var users =  await _context.Users.ToListAsync();

            return _mapper.Map<ICollection<UserVM>>(users);
        }

        public Task<UserVM> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserVM> UpdateUserAsync(UserVM user)
        {
            throw new NotImplementedException();
        }
    }
}
