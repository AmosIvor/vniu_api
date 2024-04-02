using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Promotions;
using vniu_api.Repositories;
using vniu_api.Repositories.Promotions;
using vniu_api.ViewModels.PromotionsViewModels;

namespace vniu_api.Services.Promotions
{
    public class PromotionRepo : IPromotionRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PromotionRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PromotionVM> CreatePromotionAsync(PromotionVM promotionVM)
        {
            var promotionMap = _mapper.Map<Promotion>(promotionVM);

            _context.Promotions.Add(promotionMap);

            await _context.SaveChangesAsync();

            var newPromotionVM = _mapper.Map<PromotionVM>(promotionMap);

            return newPromotionVM;
        }

        public async Task<PromotionVM> GetPromotionByIdAsync(int promotionId)
        {
            var promotion = await _context.Promotions.SingleOrDefaultAsync(p => p.Id == promotionId);

            return _mapper.Map<PromotionVM>(promotion);
        }

        public async Task<ICollection<PromotionVM>> GetPromotionsAsync()
        {
            var promotions = await _context.Promotions.OrderBy(p => p.Id).ToListAsync();

            return _mapper.Map<ICollection<PromotionVM>>(promotions);
        }

        public async Task<bool> IsPromotionExistIdAsync(int promotionId)
        {
            return await _context.Promotions.AnyAsync(p => p.Id == promotionId);
        }

        public async Task<bool> IsPromotionExistNameAsync(string promotionName)
        {
            var promotion = await _context.Promotions.Where(p => p.Name.Trim().ToUpper() == promotionName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return promotion != null;
        }
    }
}
