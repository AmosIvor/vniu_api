namespace vniu_api.ViewModels.ProductsViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual ICollection<ProductItemVM> ProductItems { get;set; }
    }
}
