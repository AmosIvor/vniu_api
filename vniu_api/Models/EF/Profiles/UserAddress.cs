using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Profiles
{
    [Table("User_Address")]
    public class UserAddress
    {
        // UserId
        public string UserId { get; set; }

        public int AddressId { get; set; }

        public Boolean IsDefault { get; set; } = false;

        public User User {  get; set; }

        public Address Address {  get; set; }
    }
}
