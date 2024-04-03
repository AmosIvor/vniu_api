using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface ISizeRepo
    {
        public Task<ICollection<SizeVM>> GetSizesAsync();
        public Task<SizeVM> GetSizeByIdAsync(int sizeId);
        public Task<bool> IsSizeExistIdAsync(int sizeId);
        public Task<bool> IsSizeExistNameAsync(string sizeName);
        public Task<SizeVM> CreateSizeAsync(SizeVM sizeVM);
    }
}
