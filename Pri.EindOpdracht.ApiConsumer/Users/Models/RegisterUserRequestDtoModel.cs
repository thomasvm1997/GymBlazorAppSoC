using Pri.Ee.Api.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Users.Models
{
    public class RegisterUserRequestDtoModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long. Must: 1 capital letter, 1 number and one special character")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CustomValidators), nameof(CustomValidators.ValidateAge))]
        public DateTime BirthDate { get; set; }
    }
}
