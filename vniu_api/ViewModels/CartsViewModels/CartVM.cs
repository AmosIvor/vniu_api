
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.ViewModels.CartsViewModels
{
    public class CartVM
    {
        public int CartId { get; set; }

        public string UserId { get; set; }

        public UserVM? UserVM { get; set; }
    }
}
