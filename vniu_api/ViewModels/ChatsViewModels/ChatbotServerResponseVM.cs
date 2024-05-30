namespace vniu_api.ViewModels.ChatsViewModels
{
    public class ChatbotServerResponseVM
    {
        // response from chatbot rasa
        public string recipient_id { get; set; }
        public string? text { get; set; }
        public string? image { get; set; }
    }
}
