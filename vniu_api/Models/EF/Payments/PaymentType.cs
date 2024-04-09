using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Payments
{
    [Table("PaymentType")]
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required, MaxLength(100)]
        public string PaymentTypeValue {  get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
    }
}
