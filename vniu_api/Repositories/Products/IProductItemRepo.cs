using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IProductItemRepo
    {
        public Task<ICollection<ProductItemVM>> GetProductItemsAsync( int productId);
        public Task<ProductItemVM> GetProductItemByIdAsync(int ProductItemId);
        public Task<bool> IsProductItemExistIdAsync(int ProductItemId);
        public Task<ProductItemVM> CreateProductItemAsync(ProductItemVM ProductItemVM);
    }
}
