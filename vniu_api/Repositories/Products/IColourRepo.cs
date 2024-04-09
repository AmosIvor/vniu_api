using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Repositories.Products
{
    public interface IColourRepo
    {
        public Task<ICollection<ColourVM>> GetColoursAsync();
        public Task<ColourVM> GetColourByIdAsync(int colourId);
        public Task<bool> IsColourExistIdAsync(int colourId);
        public Task<bool> IsColourExistNameAsync(string colourName);
        public Task<ColourVM> CreateColourAsync(ColourVM colourVM);
    }
}
