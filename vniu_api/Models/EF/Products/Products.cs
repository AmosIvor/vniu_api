using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Products
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }
        [Required, Range(0, 5)]
        public double Rating { get; set; }
        //optional
        public string? ProductImage { get; set; }
        public string? ProductImage1 { get; set; }
        public string? ProductImage2 { get; set; }
        public string? ProductImage3 { get; set; }

        public Categories ParentCategories { get; set; } = new Categories();
    }
}
