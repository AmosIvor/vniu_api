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

        public int PaymentTypeId { get; set; }

        [ForeignKey("PaymentTypeId")]
        public virtual PaymentType PaymentType { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
