using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Carts
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public ICollection<CartItem> CartItems { get; set;  }

        public User User { get; set; }
    }
}
