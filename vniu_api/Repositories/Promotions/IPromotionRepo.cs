﻿using vniu_api.ViewModels.PromotionsViewModels;

namespace vniu_api.Repositories.Promotions
{
    public interface IPromotionRepo
    {
        public Task<ICollection<PromotionVM>> GetPromotionsAsync();
        public Task<PromotionVM> GetPromotionByIdAsync(int promotionId);
        public Task<bool> IsPromotionExistIdAsync(int promotionId);
        public Task<bool> IsPromotionExistNameAsync(string promotionName);
        public Task<PromotionVM> CreatePromotionAsync(PromotionVM promotionVM);
    }
}