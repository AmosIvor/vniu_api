namespace vniu_api.ViewModels.ProfilesViewModels
{
    public class UserAddressVM
    {
        public int UserId { get; set; }

        public int AddressId { get; set; }

        public Boolean IsDefault { get; set; } = false;
    }
}
