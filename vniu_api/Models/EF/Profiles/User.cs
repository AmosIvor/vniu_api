using Microsoft.AspNetCore.Identity;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Chats;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Reviews;

namespace vniu_api.Models.EF.Profiles
{
    public class User : IdentityUser
    {
        public bool? Male { get; set; }

        public DateTime? DateBirth { get; set; }

        public string? Avatar { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual Cart Cart { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
