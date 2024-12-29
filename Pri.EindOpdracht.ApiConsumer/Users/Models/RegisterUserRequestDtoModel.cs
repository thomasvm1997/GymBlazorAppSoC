using System.ComponentModel.DataAnnotations;

namespace Pri.EindOpdracht.ApiConsumer.Users.Models
{
    public class RegisterUserRequestDtoModel
    {

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
