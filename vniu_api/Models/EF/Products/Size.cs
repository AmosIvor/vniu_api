using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("Size")]
    public class Size
    {
        [Key]
        public int SizeId { get; set; }

        [Required, MaxLength(100)]
        public string SizeName { get; set; }

        public Boolean SortOrder { get; set; } = false;
    }
}
