using System.ComponentModel.DataAnnotations;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductImageVM
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
        public string url { get; set; }
        public string? ProductItemId { get; set; }

    }
}
