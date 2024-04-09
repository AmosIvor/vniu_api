namespace vniu_api.ViewModels.OrdersViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public string? OrderNote { get; set; }

        public int OrderStatusId { get; set; }

        public int ShippingMethodId { get; set; }

        public int PromotionId { get; set; }

        public int AddressId { get; set; }

        public int PaymentMethodId { get; set; }

        public string UserId { get; set; }
    }
}
