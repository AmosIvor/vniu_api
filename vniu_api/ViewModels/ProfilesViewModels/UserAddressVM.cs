namespace vniu_api.ViewModels.ProfilesViewModels
{
    public class UserAddressVM
    {
        public string UserId { get; set; }

        public int AddressId { get; set; }

        public bool IsDefault { get; set; }

        public virtual UserVM User { get; set; }
        public virtual AddressVM Address { get; set;}

    }
}
