using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.OrdersViewModels;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Services.Products
{
    public class ProductItemRepo : IProductItemRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductItemRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductItemVM> CreateProductItemAsync(ProductItemVM ProductItemVM)
        {
            var ProductItemMap = _mapper.Map<ProductItem>(ProductItemVM);

            _context.ProductItems.Add(ProductItemMap);

            await _context.SaveChangesAsync();

            var newProductItemVM = _mapper.Map<ProductItemVM>(ProductItemMap);

            return newProductItemVM;
        }

        public async Task<ProductItemVM> GetProductItemByIdAsync(int ProductItemId)
        {
            var ProductItem = await _context.ProductItems.SingleOrDefaultAsync(p => p.ProductItemId == ProductItemId);

            return _mapper.Map<ProductItemVM>(ProductItem);
        }

        public async Task<ICollection<ProductItemVM>> GetProductItemAsync(int productId)
        {
            var ProductItems = await _context.ProductItems
                .Where(p => p.ProductId == productId)
                .ToListAsync();

            if (ProductItems == null || !ProductItems.Any())
            {
                throw new Exception("Product Items not found");
            }

            return _mapper.Map<ICollection<ProductItemVM>>(ProductItems);
        }

        public async Task<bool> IsProductItemExistIdAsync(int ProductItemId)
        {
            return await _context.ProductItems.AnyAsync(p => p.ProductItemId == ProductItemId);
        }

    }
}
