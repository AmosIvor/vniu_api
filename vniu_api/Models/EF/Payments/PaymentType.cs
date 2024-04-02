using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Payments
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }

        [Required, MaxLength(100)]
        public string PaymentTypeValue {  get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
