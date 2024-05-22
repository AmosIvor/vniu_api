using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("SizeOption")]
    public class SizeOption
    {
        [Key]
        public int SizeId { get; set; }

        [Required, MaxLength(100)]
        public string SizeName { get; set; }

        public bool SortOrder { get; set; }

        public virtual ICollection<Variation> Variations { get; set; } = new List<Variation>();
    }
}
