using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Products
{
    public class ProductOptions
    {
        [Key]
        public int Id { get; set; }

        public Products Products { get; set; } = new Products();
        public Colours Colours { get; set; } = new Colours();
        public Sizes Sizes { get; set; } = new Sizes();
        [Required]
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int Sold { get; set; }
        [Required]
        public String ImageOption { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public int CountOnSold { get; set; }
    }
}
