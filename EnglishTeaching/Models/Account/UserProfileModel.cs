using System.ComponentModel.DataAnnotations;

namespace EnglishTeaching.Models.Account
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Company { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }
        [Range(3, 100)]
        public int Age { get; set; }
    }
}
