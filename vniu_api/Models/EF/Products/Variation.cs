using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("Variation")]
    public class Variation
    {
        [Key]
        public int VariationId { get; set; }

        [Required, MaxLength(100)]
        public string ProductItemId { get; set; }
        [Required, MaxLength(100)]
        public string SizeId { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }

    }
}
