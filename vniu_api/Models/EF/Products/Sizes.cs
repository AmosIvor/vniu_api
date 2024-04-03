using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Products
{
    public class Sizes
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public Boolean SortOrder { get; set; } = false;
    }
}
