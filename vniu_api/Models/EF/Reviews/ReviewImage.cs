using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Reviews
{
    [Table("ReviewImage")]
    public class ReviewImage
    {
        [Key]
        public int ReviewImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Review Review { get; set; }
    }
}
