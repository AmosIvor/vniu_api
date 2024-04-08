using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface ISizeRepo
    {
        public Task<ICollection<SizeOptionVM>> GetSizesAsync();
        public Task<SizeOptionVM> GetSizeByIdAsync(int sizeId);
        public Task<bool> IsSizeExistIdAsync(int sizeId);
        public Task<bool> IsSizeExistNameAsync(string sizeName);
        public Task<SizeOptionVM> CreateSizeAsync(SizeOptionVM sizeVM);
    }
}
