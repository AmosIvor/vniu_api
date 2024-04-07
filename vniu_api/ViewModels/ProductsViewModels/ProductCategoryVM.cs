using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductCategoryVM
    {
        public int ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        // Optional
        public ProductCategoryVM? ParentProductCategory { get; set; } = new ProductCategoryVM();
    }
}
