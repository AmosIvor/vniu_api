using vniu_api.Models.EF.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.ViewModels.CartsViewModels
{
    public class CartItemVM
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }

        public int CartId { get; set; }

        public int ProductItemId { get; set; }

        public virtual ProductItemVM? ProductItemVM { get; set; }

        public int VariationId { get; set; }
        public virtual VariationVM? VariationVM { get; set; }

    }
}
