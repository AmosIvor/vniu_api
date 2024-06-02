namespace vniu_api.ViewModels.ProductsViewModels
{
    public class VariationVM
    {
        public int VariationId { get; set; }

        public int QuantityInStock { get; set; }

        public int ProductItemId { get; set; }

        public int SizeId { get; set; }

        public SizeOptionVM Size { get; set; }
    }
}
