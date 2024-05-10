using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Chats
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string MessageContent { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public bool IsFromUser { get; set; } = true;

        public DateTime MessageCreateAt { get; set; }

        public DateTime? MessageReadAt { get; set; }

        public bool IsRead { get; set; } = false;

        public int ChatRoomId { get; set; }

        [ForeignKey("ChatRoomId")]
        public ChatRoom ChatRoom { get; set; }
    }
}
