using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Products;
using vniu_api.Models.EF.Reviews;

namespace vniu_api.Models.EF.Orders
{
    [Table("OrderLine")]
    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required]
        public decimal Price { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        public virtual Review? Review { get; set; }

        public int ProductItemId { get; set; }

        [ForeignKey("ProductItemId")]
        public virtual ProductItem? ProductItem { get; set; }

        public int VariationId { get; set; }

        [ForeignKey("VariationId")]
        public virtual Variation? Variation { get; set; }
    }
}
