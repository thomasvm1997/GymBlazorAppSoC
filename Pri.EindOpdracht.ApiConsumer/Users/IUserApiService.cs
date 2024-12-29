using Pri.EindOpdracht.ApiConsumer.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.Users
{
    public interface IUserApiService
    {
        Task<LoginUserResponseDtoModel> LoginAsync(LoginUserRequestDtoModel userToLogin);
        Task<RegisterUserResponseDtoModel> RegisterAsync(RegisterUserRequestDtoModel userToRegister);
    }
}
