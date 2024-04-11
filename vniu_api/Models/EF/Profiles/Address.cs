using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Orders;

namespace vniu_api.Models.EF.Profiles
{
    [Table("Address")]
    public class Address
    {
        [Key]
        public int AddressId {  get; set; }

        [MaxLength(255)]
        public int? UnitNumber {  get; set; }

        [MaxLength(50)]
        public string? StreetNumber { get; set; }

        [MaxLength(255)]
        public string? AddressLine1 { get; set; }

        [MaxLength(255)]
        public string? AddressLine2 { get; set; }

        [Required, MaxLength(255)]
        public string District { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
