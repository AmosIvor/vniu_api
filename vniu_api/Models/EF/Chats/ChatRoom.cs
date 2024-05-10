using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Chats
{
    [Table("ChatRoom")]
    public class ChatRoom
    {
        [Key]
        public int ChatRoomId { get; set; }

        // Foreign Key
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
