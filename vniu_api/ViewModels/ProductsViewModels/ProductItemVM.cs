using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductItemVM
    {
        public int ProductItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Add ProductName here
        public int ColourId { get; set; }
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int ProductItemSold { get; set; }
        public double ProductItemRating { get; set; }
        public int ProductItemCode { get; set; }
        public ProductImageVM ProductImage { get; set; }

        public virtual ICollection<ProductImageVM> ProductImages { get; set; }
        public virtual ICollection<VariationVM>? Variations { get; set; }        
        public virtual ICollection<ColourVM>? ColourVMs { get; set; }

    }
}
