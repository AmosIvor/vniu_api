using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IProductOptionRepo
    {
        public Task<ICollection<ProductOptionVM>> GetproductOptionsAsync( int productId);
        public Task<ProductOptionVM> GetProductOptionByIdAsync(int productOptionId);
        public Task<bool> IsProductOptionExistIdAsync(int productOptionId);
        public Task<ProductOptionVM> CreateProductOptionAsync(ProductOptionVM productOptionVM);
    }
}
