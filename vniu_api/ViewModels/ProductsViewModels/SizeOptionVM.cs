using System.ComponentModel.DataAnnotations;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class SizeOptionVM
    {
        public int SizeId { get; set; }

        public string SizeName { get; set; }

        public bool SortOrder { get; set; }
    }
}
