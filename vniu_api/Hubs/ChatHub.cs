using Microsoft.AspNetCore.SignalR;
using vniu_api.ViewModels.ChatsViewModels;

namespace vniu_api.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public override Task OnConnectedAsync()
        {
            //await Clients.All.ReceiveMessage($"has joined");
            Console.WriteLine($"{Context.ConnectionId} has joined to ChatHub");

            return base.OnConnectedAsync();
        }

        public async Task ReceiveMessageAsync(MessageVM messageVM)
        {
            await Clients.All.ReceiveMessage(messageVM);
        }
    }
}
