using vniu_api.Constants;

namespace vniu_api.ViewModels.RequestsViewModels
{
    public class PaymentRequest
    {
        public string? OrderType { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderDescription { get; set; }
        public string UserId { get; set; }
        public bool IsVnPay { get; set; } = true;
    }
}
