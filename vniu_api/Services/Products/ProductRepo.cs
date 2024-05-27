using AutoMapper;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories.Products;
using vniu_api.Repositories;
using vniu_api.ViewModels.ProductsViewModels;
using Microsoft.EntityFrameworkCore;
using vniu_api.Exceptions;
using vniu_api.ViewModels.CartsViewModels;
using vniu_api.ViewModels.ProfilesViewModels;

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
            // Eagerly load the necessary data
            var products = await _context.Products
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.ProductImages)
                .OrderBy(p => p.ProductId)
                .ToListAsync();

            // Map to ProductVM
            var productVMs = products.Select(product => new ProductVM
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductCategoryId = product.ProductCategoryId,
                ProductItems = product.ProductItems.Take(1).Select(item => new ProductItemVM
                {
                    ProductItemId = item.ProductItemId,
                    ProductId = item.ProductId,
                    ColourId = item.ColourId,
                    OriginalPrice = item.OriginalPrice,
                    SalePrice = item.SalePrice,
                    ProductItemSold = item.ProductItemSold,
                    ProductItemRating = item.ProductItemRating,
                    ProductItemCode = item.ProductItemCode,
                    ProductImage = item.ProductImages.Take(1).Select(image => new ProductImageVM
                    {
                        ProductImageId = image.ProductImageId,
                        ProductImageUrl = image.ProductImageUrl,
                        ProductItemId = image.ProductItemId
                    }).FirstOrDefault()
                }).ToList()
            }).ToList();

            return productVMs;
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
