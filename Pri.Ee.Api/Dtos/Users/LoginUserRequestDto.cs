using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Api.Dtos.Users
{
    public class LoginUserRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
