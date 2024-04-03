using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Services.Products
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryVM> CreateCategoryAsync(CategoryVM CategoryVM)
        {
            var CategoryMap = _mapper.Map<Category>(CategoryVM);

            _context.Categories.Add(CategoryMap);

            await _context.SaveChangesAsync();

            var newCategoryVM = _mapper.Map<CategoryVM>(CategoryMap);

            return newCategoryVM;
        }

        public async Task<CategoryVM> GetCategoryByIdAsync(int CategoryId)
        {
            var Category = await _context.Categories.SingleOrDefaultAsync(p => p.CategoryId == CategoryId);

            return _mapper.Map<CategoryVM>(Category);
        }

        public async Task<ICollection<CategoryVM>> GetCategorysAsync()
        {
            var Categorys = await _context.Categories.OrderBy(p => p.CategoryId).ToListAsync();

            return _mapper.Map<ICollection<CategoryVM>>(Categorys);
        }

        public async Task<bool> IsCategoryExistIdAsync(int CategoryId)
        {
            return await _context.Categories.AnyAsync(p => p.CategoryId == CategoryId);
        }

        public async Task<bool> IsCategoryExistNameAsync(string CategoryName)
        {
            var Category = await _context.Categories.Where(p => p.CategoryName.Trim().ToUpper() == CategoryName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return Category != null;
        }
    }
}
