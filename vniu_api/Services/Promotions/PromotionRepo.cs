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

        public async Task<PromotionVM> DeletePromotionAsync(int promotionId)
        {
            var promotionDelete = _context.Promotions.SingleOrDefault(p => p.PromotionId == promotionId);

            if (promotionDelete == null)
            {
                throw new Exception("Promotion not found");
            }

            _context.Promotions.Remove(promotionDelete);

            await _context.SaveChangesAsync();

            var promotionDeleteVM = _mapper.Map<PromotionVM>(promotionDelete);
            
            return promotionDeleteVM;
        }

        public async Task<PromotionVM> GetPromotionByIdAsync(int promotionId)
        {
            var promotion = await _context.Promotions.SingleOrDefaultAsync(p => p.PromotionId == promotionId);

            if (promotion == null)
            {
                throw new Exception("Promotion not found");
            }

            var promotionVM = _mapper.Map<PromotionVM>(promotion);

            return promotionVM;
        }

        public async Task<ICollection<PromotionVM>> GetPromotionsAsync()
        {
            var promotions = await _context.Promotions.OrderBy(p => p.PromotionId).ToListAsync();

            var promotionsVM = _mapper.Map<ICollection<PromotionVM>>(promotions);

            return promotionsVM;
        }

        public async Task<bool> IsPromotionExistIdAsync(int promotionId)
        {
            return await _context.Promotions.AnyAsync(p => p.PromotionId == promotionId);
        }

        public async Task<bool> IsPromotionExistNameAsync(string promotionName)
        {
            var promotion = await _context.Promotions.Where(p => p.PromotionName.Trim().ToUpper() == promotionName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return promotion != null;
        }

        public async Task<PromotionVM> UpdatePromotionAsync(int promotionId, PromotionVM promotionVM)
        {
            if (promotionId != promotionVM.PromotionId)
            {
                throw new Exception("Promotion Id is different");
            }

            var isExist = await IsPromotionExistIdAsync(promotionId);

            if (isExist == false)
            {
                throw new Exception("Promotion not found");
            }

            var promotionUpdate = _mapper.Map<Promotion>(promotionVM);

            _context.Promotions.Update(promotionUpdate);

            await _context.SaveChangesAsync();

            var promotionUpdateVM = _mapper.Map<PromotionVM>(promotionUpdate);

            return promotionUpdateVM;
        }
    }
}
