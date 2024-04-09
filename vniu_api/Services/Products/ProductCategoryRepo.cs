using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Services.Products
{
    public class ProductCategoryRepo : IProductCategoryRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductCategoryVM> CreateCategoryAsync(ProductCategoryVM CategoryVM)
        {
            var CategoryMap = _mapper.Map<ProductCategory>(CategoryVM);

            _context.Categories.Add(CategoryMap);

            await _context.SaveChangesAsync();

            var newCategoryVM = _mapper.Map<ProductCategoryVM>(CategoryMap);

            return newCategoryVM;
        }

        public async Task<ProductCategoryVM> GetCategoryByIdAsync(int CategoryId)
        {
            var Category = await _context.Categories.SingleOrDefaultAsync(p => p.ProductCategoryId == CategoryId);

            return _mapper.Map<ProductCategoryVM>(Category);
        }

        public async Task<ICollection<ProductCategoryVM>> GetCategorysAsync()
        {
            var Categorys = await _context.Categories.OrderBy(p => p.ProductCategoryId).ToListAsync();

            return _mapper.Map<ICollection<ProductCategoryVM>>(Categorys);
        }

        public async Task<bool> IsCategoryExistIdAsync(int CategoryId)
        {
            return await _context.Categories.AnyAsync(p => p.ProductCategoryId == CategoryId);
        }

        public async Task<bool> IsCategoryExistNameAsync(string CategoryName)
        {
            var Category = await _context.Categories.Where(p => p.ProductCategoryName.Trim().ToUpper() == CategoryName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return Category != null;
        }
    }
}
