using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Carts
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
    }
}
