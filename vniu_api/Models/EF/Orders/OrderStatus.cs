using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Orders
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [Required, MaxLength(50)]
        public string OrderStatusName { get; set;}

        public ICollection<Order> Orderes {  get; set; }
    }
}
