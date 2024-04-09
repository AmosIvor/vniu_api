using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("Variation")]
    public class Variation
    {
        [Key]
        public int VariationId { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public int ProductItemId { get; set; }

        [ForeignKey("ProductItemId")]
        public virtual ProductItem ProductItem { get; set; } = new ProductItem();

        public int SizeId { get; set; }

        [ForeignKey("SizeId")]
        public virtual SizeOption SizeOption { get; set; } = new SizeOption();
    }
}
