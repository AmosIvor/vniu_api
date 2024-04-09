using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductItem")]
    public class ProductItem
    {
        [Key]
        public int ProductItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ColourId { get; set; }

        public int VariationId { get; set; }
        public Product Product { get; set; } = new Product();
        public Colour Colour { get; set; } = new Colour();
        [Required]
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int Sold { get; set; }
        [Required]
        public double Rating { get; set; }
    }
}
