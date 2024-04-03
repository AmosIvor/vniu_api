using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductOption")]
    public class ProductOption
    {
        [Key]
        public int ProductOptionId { get; set; }

        public Product Products { get; set; } = new Product();
        public Colour Colours { get; set; } = new Colour();
        public Size Sizes { get; set; } = new Size();
        [Required]
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int Sold { get; set; }
        [Required]
        public String ImageOption { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public int CountOnSold { get; set; }
    }
}
