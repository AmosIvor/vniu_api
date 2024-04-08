using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IProductRepo
    {
        public Task<ICollection<ProductVM>> GetProductsAsync();
        public Task<ProductVM> GetProductByIdAsync(int productId);
        public Task<bool> IsProductExistIdAsync(int productId);
        public Task<bool> IsProductExistNameAsync(string productName);
        public Task<ProductVM> CreateProductAsync(ProductVM productVM);
    }
}
