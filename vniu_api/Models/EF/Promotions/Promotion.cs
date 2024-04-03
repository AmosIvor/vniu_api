using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Orders;

namespace vniu_api.Models.EF.Promotions
{
    [Table("Promotion")]
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required, MaxLength(100)]
        public string PromotionName { get; set; }

        [Required]
        public DateTime PromotionStartDate { get; set; }

        [Required]
        public DateTime PromotionEndDate { get; set; }

        [Required, Range(0, 100)]
        public int PromotionDiscountRate { get; set; }

        [MaxLength(300)]
        public string? PromotionDescription { get; set; }

        public Boolean PromotionIsUsed { get; set; } = false;

        public ICollection<Order> Orders { get; set; }
    }
}
