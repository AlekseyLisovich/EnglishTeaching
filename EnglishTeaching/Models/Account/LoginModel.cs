using System.ComponentModel.DataAnnotations;

namespace EnglishTeaching.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
