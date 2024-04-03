namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductVM
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Rating { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductImage1 { get; set; }
        public string? ProductImage2 { get; set; }
        public string? ProductImage3 { get; set; }
        public CategoryVM category { get; set; } = new CategoryVM();

        public ICollection<ProductOptionVM> Products { get; set; }
    }
}
