using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Products
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        // Optional
        public Categories? ParentCategories { get; set; } = new Categories();
    }
}
