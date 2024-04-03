using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface ICategoryRepo
    {
        public Task<ICollection<CategoryVM>> GetCategorysAsync();
        public Task<CategoryVM> GetCategoryByIdAsync(int categoryId);
        public Task<bool> IsCategoryExistIdAsync(int categoryId);
        public Task<bool> IsCategoryExistNameAsync(string categoryName);
        public Task<CategoryVM> CreateCategoryAsync(CategoryVM categoryVM);
    }
}
