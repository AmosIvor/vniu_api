using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        public int ProductItemId { get; set; }
        [Required]
        public string url { get; set; }
    }
}
