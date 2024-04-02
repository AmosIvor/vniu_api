using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Reviews
{
    public class ReviewImage
    {
        [Key]
        public int ReviewImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Review Review { get; set; }
    }
}
