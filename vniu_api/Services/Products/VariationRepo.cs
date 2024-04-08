using AutoMapper;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories.Products;
using vniu_api.Repositories;
using vniu_api.ViewModels.ProductsViewModels;
using Microsoft.EntityFrameworkCore;

namespace vniu_api.Services.Products
{
    public class VariationRepo : IVariationRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VariationRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VariationVM> CreateVariationAsync(VariationVM VariationVM)
        {
            var VariationMap = _mapper.Map<Variation>(VariationVM);

            _context.Variations.Add(VariationMap);

            await _context.SaveChangesAsync();

            var newVariationVM = _mapper.Map<VariationVM>(VariationMap);

            return newVariationVM;
        }

        public async Task<VariationVM> GetVariationByIdAsync(int VariationId)
        {
            var Variation = await _context.Variations.SingleOrDefaultAsync(p => p.VariationId == VariationId);

            return _mapper.Map<VariationVM>(Variation);
        }

        public async Task<ICollection<VariationVM>> GetVariationsAsync()
        {
            var Variations = await _context.Variations.OrderBy(p => p.VariationId).ToListAsync();

            return _mapper.Map<ICollection<VariationVM>>(Variations);
        }

        public async Task<bool> IsVariationExistIdAsync(int VariationId)
        {
            return await _context.Variations.AnyAsync(p => p.VariationId == VariationId);
        }

    }
}
