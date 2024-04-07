using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IProductImageRepo
    {
        public Task<ICollection<ProductImageVM>> GetProductImagesAsync();
        public Task<ProductImageVM> GetProductImageByIdAsync(int ProductImageId);
        public Task<bool> IsProductImageExistIdAsync(int ProductImageId);
        public Task<ProductImageVM> CreateProductImageAsync(ProductImageVM ProductImageVM);
    }
}
