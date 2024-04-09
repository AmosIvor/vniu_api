using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Orders
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [Required, MaxLength(50)]
        public string OrderStatusName { get; set;}

        public virtual ICollection<Order> Orderes { get; set; } = new List<Order>();
    }
}
