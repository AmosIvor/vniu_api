using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Repositories;
using vniu_api.ViewModels;
using System;

namespace vniu_api.Services
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomerRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<CustomerVM> AddCustomer(CustomerVM customer)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerVM> DeleteCustomer(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerVM>> GetAll()
        {
            var customers =  await _context.Customers.ToListAsync();

            return _mapper.Map<IEnumerable<CustomerVM>>(customers);
        }

        public Task<CustomerVM> GetCustomerById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerVM> UpdateCustomer(CustomerVM customer)
        {
            throw new NotImplementedException();
        }
    }
}
