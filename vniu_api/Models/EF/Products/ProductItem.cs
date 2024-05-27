using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Orders;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductItem")]
    public class ProductItem
    {
        [Key]
        public int ProductItemId { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }

        public decimal SalePrice { get; set; }

        public int ProductItemSold { get; set; }

        public decimal ProductItemRating { get; set; }

        [Required]
        public string ProductItemCode { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int ColourId { get; set; }

        [ForeignKey("ColourId")]
        public virtual Colour Colour { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
