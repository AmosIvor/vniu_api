using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IVariationRepo
    {
        public Task<ICollection<VariationVM>> GetVariationsAsync();
        public Task<VariationVM> GetVariationByIdAsync(int VariationId);
        public Task<bool> IsVariationExistIdAsync(int VariationId);
        public Task<bool> IsVariationExistNameAsync(string VariationName);
        public Task<VariationVM> CreateVariationAsync(VariationVM VariationVM);
    }
}
