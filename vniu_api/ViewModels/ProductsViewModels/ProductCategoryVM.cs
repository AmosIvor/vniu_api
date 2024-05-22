using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductCategoryVM
    {
        public int ProductCategoryId { get; set; }

        public string? ProductCategoryName { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
