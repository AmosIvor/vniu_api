using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF
{
    public class User : IdentityUser
    {
        public bool? Male { get; set; }

        public DateTime? DateBirth { get; set; }

        public string? Avatar { get; set; }
    }
}
