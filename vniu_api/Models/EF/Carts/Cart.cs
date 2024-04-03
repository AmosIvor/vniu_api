using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Carts
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set;  }
    }
}
