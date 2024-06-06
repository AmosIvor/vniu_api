namespace vniu_api.ViewModels.OrdersViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        public DateTime OrderCreateAt { get; set; }

        public DateTime OrderUpdateAt { get; set; }

        public decimal OrderTotal { get; set; }

        public string? OrderNote { get; set; }

        public int OrderStatusId { get; set; }

        public int ShippingMethodId { get; set; }

        public int? PromotionId { get; set; }

        public string Address { get; set; }

        public string Username { get; set; }
        public string NumberPhone { get; set; }

        public int? PaymentMethodId { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<OrderLineVM>? OrderLines{ get;set; }
    }
}
