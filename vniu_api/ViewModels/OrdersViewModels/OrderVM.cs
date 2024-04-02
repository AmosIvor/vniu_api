namespace vniu_api.ViewModels.OrdersViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public string? OrderNote { get; set; }
    }
}
