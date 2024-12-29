using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Users.Models
{
    public class LoginUserRequestDtoModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
