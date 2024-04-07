using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }

        [Required, MaxLength(100)]
        public string ProductCategoryName { get; set; }

        // Optional
        public ProductCategory? ParentProductCategory { get; set; } = new ProductCategory();
    }
}
