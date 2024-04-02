namespace vniu_api.ViewModels.PromotionsViewModels
{
    public class PromotionVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DiscountRate { get; set; }

        public string? Description { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
