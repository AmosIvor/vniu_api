using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Products;

namespace vniu_api.Models.EF.Promotions
{
    [Table("PromotionCategory")]
    public class PromotionCategory
    {
        public int PromotionId { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; } = new ProductCategory();

        public virtual Promotion Promotion { get; set; }
    }
}
