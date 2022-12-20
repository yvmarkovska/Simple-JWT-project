using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "The user's {0} must be between {1} and {2} characters long!")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
