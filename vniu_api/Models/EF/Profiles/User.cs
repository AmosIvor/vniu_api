using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Payments;
using vniu_api.Models.EF.Reviews;

namespace vniu_api.Models.EF.Profiles
{
    public class User : IdentityUser
    {
        public bool? Male { get; set; }

        public DateTime? DateBirth { get; set; }

        public string? Avatar { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Cart Cart { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
