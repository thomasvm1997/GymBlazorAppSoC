using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Users.Models
{
    public class LoginUserRequestDtoModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
