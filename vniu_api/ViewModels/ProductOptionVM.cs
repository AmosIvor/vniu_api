using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels
{
    public class ProductOptionVM
    {
        public int Id { get; set; }
        public Colours Colours { get; set; } = new Colours();
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int Sold { get; set; }
        public string ImageOption { get; set; }
        public int QuantityInStock { get; set; }
        public int CountOnSold { get; set; }
    }
}
