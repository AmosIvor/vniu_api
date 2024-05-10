using vniu_api.ViewModels.ChatsViewModels;

namespace vniu_api.Repositories.Chats
{
    public interface IMessageRepo
    {
        Task<ICollection<MessageVM>> GetMessagesByUserIdAsync(string userId);
        Task<MessageVM> CreateMessageAsync(string userId, MessageVM messageVM);
        Task<MessageVM> ReadMesasgeAsync(int messageId);
    }
}
