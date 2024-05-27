using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductImageVM
    {
        public int ProductImageId { get; set; }

        public string ProductImageUrl { get; set; }

        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; }

    }
}
