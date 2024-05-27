using vniu_api.ViewModels.ChatsViewModels;

namespace vniu_api.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageVM messageVM);
    }
}
