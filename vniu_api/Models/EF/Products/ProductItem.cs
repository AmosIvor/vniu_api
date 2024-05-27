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
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = new Product();
        [Required]
        public int ColourId { get; set; }

        public int OriginalPrice { get; set; }

        public int SalePrice { get; set; }

        public int ProductItemSold { get; set; }

        public double ProductItemRating { get; set; }

        public int ProductItemCode { get; set; }

        //public virtual Product Product { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
