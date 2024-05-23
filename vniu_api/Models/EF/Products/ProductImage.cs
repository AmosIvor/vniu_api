using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ProductImageUrl { get; set; }

        public int ProductItemId { get; set; }

        [ForeignKey("ProductItemId")]
        public virtual ProductItem ProductItem { get; set; } 
    }
}
