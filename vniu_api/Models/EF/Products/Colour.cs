using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("Colour")]
    public class Colour
    {
        [Key]
        public int ColourId { get; set; }

        [Required, MaxLength(100)]
        public string ColourName { get; set; }

        public virtual ICollection<ProductItem> ProductItems { get; set; }
    }
}
