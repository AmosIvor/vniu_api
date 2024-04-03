using System.ComponentModel.DataAnnotations;

namespace vniu_api.ViewModels.ProductsViewModels
{
    public class SizeVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool SortOrder { get; set; }
    }
}
