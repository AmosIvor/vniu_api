using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Utils
{
    [Table("Photo")]
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Required]
        [MaxLength(200)]
        public string PhotoPublicId { get; set; }

        [Required]
        [MaxLength(200)]
        public string PhotoUrl { get; set; }
    }
}
