using vniu_api.Models;
using vniu_api.ViewModels;

namespace vniu_api.Repositories
{
    public interface IUserRepo
    {
        Task<ICollection<UserVM>> GetUsersAsync();
        Task<UserVM> GetUserByIdAsync(string id);
        Task<UserVM> CreateUserAsync(UserVM user);
        Task<UserVM> UpdateUserAsync(UserVM user);
        Task<UserVM> DeleteUserAsync(string id);
    }
}
