using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Promotions;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }

        [Required, MaxLength(100)]
        public string ProductCategoryName { get; set; }

        public int ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual ProductCategory ParentCategory { get; set; }

        public virtual ICollection<ProductCategory> ChildProductCategories { get; set; } 

        public virtual ICollection<PromotionCategory> PromotionCategories { get; set; } 
        public virtual ICollection<Product> Products { get; set; }
    }
}
