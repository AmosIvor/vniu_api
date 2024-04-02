using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Orders;

namespace vniu_api.Models.EF.Shippings
{
    public class ShippingMethod
    {
        [Key]
        public int ShippingMethodId {  get; set; }

        [Required, MaxLength(100)]
        public string ShippingMethodName { get; set; }

        [Required]
        public string ShippingMethodPrice {  get; set; }

        public ICollection<Order> Orderes {  get; set; }
    }
}
