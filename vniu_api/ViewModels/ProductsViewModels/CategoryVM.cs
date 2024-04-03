using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Optional
        public CategoryVM? ParentCategories { get; set; } = new CategoryVM();
    }
}
