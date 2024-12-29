using Pri.EindOpdracht.ApiConsumer.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.Users
{
    public class UserApiService : IUserApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _workoutApiClient;
        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _workoutApiClient = _httpClientFactory.CreateClient("WorkoutApiClient");
            _workoutApiClient.BaseAddress = new Uri(ApiRoutes.Auth);
        }
        public async Task<LoginUserResponseDtoModel> LoginAsync(LoginUserRequestDtoModel userToLogin)
        {
            var response = await _workoutApiClient.PostAsJsonAsync("login", userToLogin);

            if (response.IsSuccessStatusCode)
            {
                LoginUserResponseDtoModel user = await response.Content.ReadFromJsonAsync<LoginUserResponseDtoModel>();

                return user;
            }

            return null;
        }

        public async Task<RegisterUserResponseDtoModel> RegisterAsync(RegisterUserRequestDtoModel userToRegister)
        {
            var response = await _workoutApiClient.PostAsJsonAsync("register", userToRegister);

            if (response.IsSuccessStatusCode)
            {
                RegisterUserResponseDtoModel user = await response.Content.ReadFromJsonAsync<RegisterUserResponseDtoModel>();

                return user;
            }

            return null;
        }
    }
}
