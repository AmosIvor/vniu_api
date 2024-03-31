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

        public Task<UserVM> AddUser(UserVM user)
        {
            throw new NotImplementedException();
        }

        public Task<UserVM> DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserVM>> GetAll()
        {
            var users =  await _context.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserVM>>(users);
        }

        public Task<UserVM> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserVM> UpdateUser(UserVM user)
        {
            throw new NotImplementedException();
        }
    }
}
