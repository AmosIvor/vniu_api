using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Orders;

namespace vniu_api.Models.EF.Products
{
    [Table("Variation")]
    public class Variation
    {
        [Key]
        public int VariationId { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public int ProductItemId { get; set; }

        [ForeignKey("ProductItemId")]
        public virtual ProductItem ProductItem { get; set; }
        public int SizeId { get; set; }

        [ForeignKey("SizeId")]
        public virtual SizeOption Size { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
