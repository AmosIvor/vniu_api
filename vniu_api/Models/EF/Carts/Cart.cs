﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vniu_api.Models.EF.Profiles;

namespace vniu_api.Models.EF.Carts
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        // Foreign Key
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set;  } = new List<CartItem>();
    }
}
