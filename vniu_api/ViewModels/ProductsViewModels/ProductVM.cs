using vniu_api.ViewModels.ReviewsViewModels;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductVM
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }        
        public ProductCategoryVM ProductCategory { get; set; } = new ProductCategoryVM();

        public ICollection<ProductItemVM>? ProductItemVMs { get; set; }
        public ICollection<ReviewVM>? ReviewVMs { get; set; }
    }
}
