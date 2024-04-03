using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public Order Order { get; set; }

        public Review Review { get; set; }
    }
}
