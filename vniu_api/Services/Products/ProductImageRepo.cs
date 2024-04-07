using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Services.Products
{
    public class ProductImageRepo: IProductImageRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductImageRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductImageVM> GetProductImageByIdAsync(int ProductImageId)
        {
            var ProductImage = await _context.ProductImages.SingleOrDefaultAsync(p => p.ImageId == ProductImageId);

            return _mapper.Map<ProductImageVM>(ProductImage);
        }

        public async Task<ICollection<ProductImageVM>> GetProductImagesAsync()
        {
            var ProductImages = await _context.ProductImages.OrderBy(p => p.ImageId).ToListAsync();

            return _mapper.Map<ICollection<ProductImageVM>>(ProductImages);
        }

        public async Task<bool> IsProductImageExistIdAsync(int ProductImageId)
        {
            return await _context.ProductImages.AnyAsync(p => p.ImageId == ProductImageId);
        }
        public async Task<ProductImageVM> CreateProductImageAsync(ProductImageVM ProductImageVM)
        {
            var ProductImageMap = _mapper.Map<ProductImage>(ProductImageVM);

            _context.ProductImages.Add(ProductImageMap);

            await _context.SaveChangesAsync();

            var newProductImageVM = _mapper.Map<ProductImageVM>(ProductImageMap);

            return newProductImageVM;
        }

    }
}
