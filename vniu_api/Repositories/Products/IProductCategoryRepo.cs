using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IProductCategoryRepo
    {
        public Task<ICollection<ProductCategoryVM>> GetCategorysAsync();
        public Task<ProductCategoryVM> GetCategoryByIdAsync(int categoryId);
        public Task<bool> IsCategoryExistIdAsync(int categoryId);
        public Task<bool> IsCategoryExistNameAsync(string categoryName);
        public Task<ProductCategoryVM> CreateCategoryAsync(ProductCategoryVM categoryVM);
    }
}
