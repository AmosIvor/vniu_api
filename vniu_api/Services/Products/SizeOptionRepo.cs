using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Services.Products
{
    public class SizeOptionRepo : ISizeOptionRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SizeOptionRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SizeOptionVM> CreateSizeAsync(SizeOptionVM sizeVM)
        {
            var sizeMap = _mapper.Map<SizeOption>(sizeVM);

            _context.SizeOptions.Add(sizeMap);

            await _context.SaveChangesAsync();

            var newsizeVM = _mapper.Map<SizeOptionVM>(sizeMap);

            return newsizeVM;
        }

        public async Task<SizeOptionVM> GetSizeByIdAsync(int sizeId)
        {
            var size = await _context.SizeOptions.SingleOrDefaultAsync(p => p.SizeId == sizeId);

            return _mapper.Map<SizeOptionVM>(size);
        }

        public async Task<ICollection<SizeOptionVM>> GetSizeOptionsAsync()
        {
            var sizes = await _context.SizeOptions.OrderBy(p => p.SizeId).ToListAsync();

            return _mapper.Map<ICollection<SizeOptionVM>>(sizes);
        }

        public async Task<bool> IsSizeExistIdAsync(int sizeId)
        {
            return await _context.SizeOptions.AnyAsync(p => p.SizeId == sizeId);
        }

        public async Task<bool> IsSizeExistNameAsync(string sizeName)
        {
            var size = await _context.SizeOptions.Where(p => p.SizeName.Trim().ToUpper() == sizeName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return size != null;
        }
    }
}
