using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public string ImageId { get; set; }
        [Required]
        public string ProductItemId { get; set; }
        [Required]
        public string url { get; set; }
    }
}
