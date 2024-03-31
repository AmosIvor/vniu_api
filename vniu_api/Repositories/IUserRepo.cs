using vniu_api.Models;
using vniu_api.ViewModels;

namespace vniu_api.Repositories
{
    public interface IUserRepo
    {
        Task<IEnumerable<UserVM>> GetAll();
        Task<UserVM> GetUserById(string id);
        Task<UserVM> AddUser(UserVM user);
        Task<UserVM> UpdateUser(UserVM user);
        Task<UserVM> DeleteUser(string id);
    }
}
