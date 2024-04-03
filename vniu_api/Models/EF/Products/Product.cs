using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Products
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(300)]
        public string? ProductDescription { get; set; }
        [Required, Range(0, 5)]
        public double ProductRating { get; set; }
        //optional
        public string? ProductImage { get; set; }
        public string? ProductImage1 { get; set; }
        public string? ProductImage2 { get; set; }
        public string? ProductImage3 { get; set; }

        public Category ParentCategories { get; set; } = new Category();
    }
}
