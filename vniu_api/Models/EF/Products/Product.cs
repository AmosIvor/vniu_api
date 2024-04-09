using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(300)]
        public string? ProductDescription { get; set; }

        public int CategoryId { get; set; }

        public ICollection<ProductItem> ProductItems { get; set; }

        public ProductCategory Category { get; set; } = new ProductCategory();
    }
}
