using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace vniu_api.ViewModels
{
    public class PromotionVM
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DiscountRate { get; set; }

        public string? Description { get; set; }

        public Boolean IsUsed { get; set; } = false;
    }
}
