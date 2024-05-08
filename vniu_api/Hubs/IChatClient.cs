namespace vniu_api.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
