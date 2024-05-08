using vniu_api.ViewModels.ChatsViewModels;

namespace vniu_api.Repositories.Chats
{
    public interface IChatRoomRepo
    {
        Task<ICollection<ChatRoomVM>> GetChatRoomsAsync();
        Task<ChatRoomVM> GetChatRoomByUserIdAsync(string userId);
        Task<bool> IsChatRoomExist(string userId);
        Task<ChatRoomVM> CreateChatRoomByUserIdAsync(string userId);
    }
}
