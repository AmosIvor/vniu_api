namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductItemVM
    {
        public int Id { get; set; }
        public ColourVM Colour { get; set; } = new ColourVM();
        public VariationVM Variation { get; set; } = new VariationVM();
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int Sold { get; set; }
        public double Rating { get; set; }
        public ICollection<ProductImageVM> Images { get; set; }
    }
}
