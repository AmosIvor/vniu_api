using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Carts
{
    [Table("CartItem")]
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
    }
}
