using AutoMapper;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories.Products;
using vniu_api.Repositories;
using vniu_api.ViewModels.ProductsViewModels;
using Microsoft.EntityFrameworkCore;

namespace vniu_api.Services.Products
{
    public class ProductRepo : IProductRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVM> CreateProductAsync(ProductVM ProductVM)
        {
            var ProductMap = _mapper.Map<Product>(ProductVM);

            _context.Products.Add(ProductMap);

            await _context.SaveChangesAsync();

            var newProductVM = _mapper.Map<ProductVM>(ProductMap);

            return newProductVM;
        }

        public async Task<ProductVM> GetProductByIdAsync(int ProductId)
        {
            var Product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == ProductId);

            return _mapper.Map<ProductVM>(Product);
        }

        public async Task<ICollection<ProductVM>> GetProductsAsync()
        {
            var Products = await _context.Products.OrderBy(p => p.ProductId).ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(Products);
        }

        public async Task<bool> IsProductExistIdAsync(int ProductId)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == ProductId);
        }
        public async Task<bool> IsProductExistNameAsync(string ProductName)
        {
            var Product = await _context.Products.Where(p => p.ProductName.Trim().ToUpper() == ProductName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return Product != null;
        }

    }
}
