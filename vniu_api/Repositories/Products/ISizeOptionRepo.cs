using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface ISizeOptionRepo
    {
        public Task<ICollection<SizeOptionVM>> GetSizeOptionsAsync();
        public Task<SizeOptionVM> GetSizeByIdAsync(int sizeId);
        public Task<bool> IsSizeExistIdAsync(int sizeId);
        public Task<bool> IsSizeExistNameAsync(string sizeName);
        public Task<SizeOptionVM> CreateSizeAsync(SizeOptionVM sizeVM);
    }
}
