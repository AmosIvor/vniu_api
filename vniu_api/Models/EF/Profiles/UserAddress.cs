﻿using System.ComponentModel.DataAnnotations.Schema;

namespace vniu_api.Models.EF.Profiles
{
    [Table("User_Address")]
    public class UserAddress
    {
        // UserId
        public string UserId { get; set; }

        public int AddressId { get; set; }

        public bool IsDefault { get; set; } = false;

        public virtual User User { get; set; }

        public virtual Address Address { get; set; }
    }
}
