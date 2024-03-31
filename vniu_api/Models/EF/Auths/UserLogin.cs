using System.ComponentModel.DataAnnotations;

namespace vniu_api.Models.EF.Auths
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        public string? Email { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Password is required!")]
        public string? Password { get; set; }
    }
}
