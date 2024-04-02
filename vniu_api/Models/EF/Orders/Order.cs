using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Payments;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.EF.Promotions;
using vniu_api.Models.EF.Shippings;

namespace vniu_api.Models.EF.Orders
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        [MaxLength(200)]
        public string? OrderNote { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public Promotion Promotion { get; set; }

        public Address Address { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public User User { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
