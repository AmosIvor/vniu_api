namespace vniu_api.ViewModels.PromotionsViewModels
{
    public class PromotionVM
    {
        public int PromotionId { get; set; }

        public string PromotionName { get; set; }

        public DateTime PromotionStartDate { get; set; }

        public DateTime PromotionEndDate { get; set; }

        public int PromotionDiscountRate { get; set; }

        public string? PromotionDescription { get; set; }

        public bool PromotionIsUsed { get; set; } = false;
    }
}
