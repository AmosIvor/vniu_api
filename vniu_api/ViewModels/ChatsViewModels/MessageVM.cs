namespace vniu_api.ViewModels.ChatsViewModels
{
    public class MessageVM
    {
        public int MessageId { get; set; }

        public string MessageContent { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsFromUser { get; set; }

        public DateTime MessageCreateAt { get; set; }

        public DateTime? MessageReadAt { get; set; }

        public bool IsRead { get; set; }

        public int? ChatRoomId { get; set; }
    }
}
