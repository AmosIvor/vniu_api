using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Products;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductItemVM
    {
        public int ProductItemId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal SalePrice { get; set; }

        public int ProductItemSold { get; set; }

        public decimal ProductItemRating { get; set; }

        public string ProductItemCode { get; set; }

        public int ProductId { get; set; }

        public int ColourId { get; set; }
    }
}
