using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Reviews
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int ReviewRating { get; set; }

        [MaxLength(300)]
        public string? ReviewComment { get; set; }

        public DateTime ReviewCreateAt { get; set; }

        public DateTime ReviewUpdateAt { get; set; }

        public int OrderLineId { get; set; }

        public virtual OrderLine OrderLine { get; set; } = new OrderLine();

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = new User();

        public virtual ICollection<ReviewImage> ReviewImages { get; set; } = new List<ReviewImage>();
    }
}
