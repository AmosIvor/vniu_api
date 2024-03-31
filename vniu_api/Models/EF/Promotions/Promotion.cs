using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Promotions
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, Range(0, 100)]
        public int DiscountRate { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public Boolean IsUsed { get; set; } = false;
    }
}
