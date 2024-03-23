using vniu_api.Models;
using vniu_api.ViewModels;

namespace vniu_api.Repositories
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<CustomerVM>> GetAll();
        Task<CustomerVM> GetCustomerById(string id);
        Task<CustomerVM> AddCustomer(CustomerVM customer);
        Task<CustomerVM> UpdateCustomer(CustomerVM customer);
        Task<CustomerVM> DeleteCustomer(string id);
    }
}
