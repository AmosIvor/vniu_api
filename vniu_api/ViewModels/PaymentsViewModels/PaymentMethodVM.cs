namespace vniu_api.ViewModels.PaymentsViewModels
{
    public class PaymentMethodVM
    {
        public int PaymentMethodId { get; set; }

        public string? Provider { get; set; }

        public string? AccountNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Boolean? IsDefault { get; set; } = false;

        public int PaymentTypeId { get; set; }

        public string UserId { get; set; }
    }
}
