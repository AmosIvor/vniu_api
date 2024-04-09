using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Payments;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.EF.Promotions;
using vniu_api.Models.EF.Shippings;

namespace vniu_api.Models.EF.Orders
{
    [Table("Order")]
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

        public int OrderStatusId { get; set; }

        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; } = new OrderStatus();

        public int ShippingMethodId { get; set; }

        [ForeignKey("ShippingMethodId")]
        public virtual ShippingMethod ShippingMethod { get; set; } = new ShippingMethod();

        public int PromotionId { get; set; }

        [ForeignKey("PromotionId")]
        public virtual Promotion Promotion { get; set; } = new Promotion();

        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; } = new Address();

        public int PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; } = new PaymentMethod();

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = new User();

        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
