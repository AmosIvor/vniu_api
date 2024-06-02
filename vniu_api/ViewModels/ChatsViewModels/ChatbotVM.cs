namespace vniu_api.ViewModels.ChatsViewModels
{
    public class ChatbotVM
    {
        public string ChatbotId { get; set; }

        public string ChatbotContent { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsFromUser { get; set; }

        public DateTime MessageCreateAt { get; set; }
    }
}
