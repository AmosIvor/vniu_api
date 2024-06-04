using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.ViewModels.ResponsesViewModels
{
    public class AddressResponseVM : AddressVM
    {
        public int AddressId { get; set; }
        public int? UnitNumber { get; set; }
        public string? StreetNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public bool IsDefault { get; set; }

        public UserVM User { get; set; }
    }
}
