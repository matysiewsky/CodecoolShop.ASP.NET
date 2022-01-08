using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class Client
    {
        [Required]
        public int ClientId { get; set; }
        public string UserId { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The length of provided first name is not valid")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The length of provided last name is not valid")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The email address provided is not valid")]
        public string EMail { get; set; }
        [Required]
        [Range(100000000, 999999999, ErrorMessage = "The phone number provided is not valid")]
        public int PhoneNumber { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 15, ErrorMessage = "The length of provided address is not valid")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{3})")]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of provided city name is not valid")]
        public string City { get; set; }
    }
}