﻿namespace vniu_api.ViewModels.OrdersViewModels
{
    public class OrderLineVM
    {
        public int OrderLineId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public int ProductItemId { get; set; }

        public int VariationId { get; set; }
    }
}
