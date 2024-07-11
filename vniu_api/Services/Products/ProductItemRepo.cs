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

        public async Task<ProductItemVM> GetProductItemByIdAsync(int productItemId)
        {
            var productItem = await _context.ProductItems
                                            .Include(pi => pi.Product) // Include the parent Product entity
                                            .Include(pi => pi.ProductImages)
                                            .Include(pi => pi.Variations)
                                            .Include(pi => pi.Colour)
                                            .FirstOrDefaultAsync(pi => pi.ProductItemId == productItemId);

            if (productItem == null)
            {
                return null;
            }

            var productItemVM = new ProductItemVM
            {
                ProductItemId = productItem.ProductItemId,
                ProductId = productItem.Product.ProductId, // Set ProductId from the parent Product
                ProductName = productItem.Product.ProductName, // Set ProductName from the parent Product
                ColourId = productItem.ColourId,
                OriginalPrice = productItem.OriginalPrice,
                SalePrice = productItem.SalePrice,
                ProductItemSold = productItem.ProductItemSold,
                ProductItemRating = productItem.ProductItemRating,
                ProductItemCode = productItem.ProductItemCode,
                ProductImage = productItem.ProductImages.FirstOrDefault() != null ? new ProductImageVM
                {
                    ProductImageId = productItem.ProductImages.First().ProductImageId,
                    ProductImageUrl = productItem.ProductImages.First().ProductImageUrl,
                    ProductItemId = productItem.ProductImages.First().ProductItemId
                } : null,
                ProductImages = productItem.ProductImages.Select(image => new ProductImageVM
                {
                    ProductImageId = image.ProductImageId,
                    ProductImageUrl = image.ProductImageUrl,
                    ProductItemId = image.ProductItemId
                }).ToList(),
            };

            return productItemVM;
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
