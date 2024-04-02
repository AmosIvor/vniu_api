namespace vniu_api.Models.EF.Profiles
{
    public class UserAddress
    {
        public int UserId { get; set; }

        public int AddressId { get; set; }

        public Boolean IsDefault { get; set; } = false;

        public User User {  get; set; }

        public Address Address {  get; set; }
    }
}
