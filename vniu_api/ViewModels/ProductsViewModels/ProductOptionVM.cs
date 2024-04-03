namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductOptionVM
    {
        public int Id { get; set; }
        public ColourVM Colours { get; set; } = new ColourVM();
        public SizeVM size { get; set; } = new SizeVM();
        public int OriginalPrice { get; set; }
        public int SalePrice { get; set; }
        public int Sold { get; set; }
        public string ImageOption { get; set; }
        public int QuantityInStock { get; set; }
        public int CountOnSold { get; set; }
    }
}
