using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Reviews
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int ReviewRating { get; set; }

        [MaxLength(300)]
        public string? ReviewComment { get; set; }

        public OrderLine OrderLine { get; set; }

        public ICollection<ReviewImage> ReviewImages { get; set; }

        public User User {  get; set; }
    }
}
