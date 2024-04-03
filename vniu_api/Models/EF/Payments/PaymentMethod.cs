using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Payments
{
    [Table("PaymentMethod")]
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }

        [MaxLength(255)]
        public string? Provider { get; set; }

        [MaxLength(20)]
        public string? AccountNumber {  get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Boolean? IsDefault { get; set; } = false;

        public PaymentType PaymentType { get; set; }

        public ICollection<Order> Orders { get; set; }

        public User User { get; set; }
    }
}
