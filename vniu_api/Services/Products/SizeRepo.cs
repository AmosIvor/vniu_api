using AutoMapper;
using vniu_api.Models.EF.sizes;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.Repositories.sizes;
using vniu_api.ViewModels.ProductsViewModels;
using vniu_api.ViewModels.sizesViewModels;

namespace vniu_api.Services.Products
{
    public class SizeRepo : ISizeRepo
    {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public SizeRepo(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SizeVM> CreateSizeAsync(sizeVM sizeVM)
            {
                var sizeMap = _mapper.Map<size>(sizeVM);

                _context.sizes.Add(sizeMap);

                await _context.SaveChangesAsync();

                var newsizeVM = _mapper.Map<sizeVM>(sizeMap);

                return newsizeVM;
            }

            public async Task<sizeVM> GetsizeByIdAsync(int sizeId)
            {
                var size = await _context.sizes.SingleOrDefaultAsync(p => p.sizeId == sizeId);

                return _mapper.Map<sizeVM>(size);
            }

            public async Task<ICollection<sizeVM>> GetsizesAsync()
            {
                var sizes = await _context.sizes.OrderBy(p => p.sizeId).ToListAsync();

                return _mapper.Map<ICollection<sizeVM>>(sizes);
            }

            public async Task<bool> IssizeExistIdAsync(int sizeId)
            {
                return await _context.sizes.AnyAsync(p => p.sizeId == sizeId);
            }

            public async Task<bool> IssizeExistNameAsync(string sizeName)
            {
                var size = await _context.sizes.Where(p => p.sizeName.Trim().ToUpper() == sizeName.TrimEnd().ToUpper())
                    .FirstOrDefaultAsync();

                return size != null;
            }
        }
}
