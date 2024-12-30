using Pri.EindOpdracht.ApiConsumer.Goals.Models;
using Pri.EindOpdracht.ApiConsumer.Workouts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.Workouts
{
    public class WorkoutApiService : IWorkoutApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _workoutApiClient;
        public WorkoutApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _workoutApiClient = _httpClientFactory.CreateClient("WorkoutApiClient");
            _workoutApiClient.BaseAddress = new Uri(ApiRoutes.Workouts);
        }

        public async Task<ApiResult> CreateWorkoutAsync(WorkoutRequestDtoModel workoutToCreate, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.PostAsJsonAsync("", workoutToCreate);

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not create object" };
            }

            return new ApiResult { Success = true };
        }

        public async Task<ApiResult> DeleteWorkoutAsync(int id, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not delete object" };
            }

            return new ApiResult { Success = true };
        }

        public async Task<WorkoutResponseDtoModel> GetWorkoutByIdAsync(int id, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var product = await _workoutApiClient.GetFromJsonAsync<WorkoutResponseDtoModel>($"{id}");
            return product;
        }

        public async Task<WorkoutResponseDtoModel[]> GetWorkoutsAsync(string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await _workoutApiClient.GetFromJsonAsync<WorkoutResponseDtoModel[]>("");

            if (products is not null)
            {
                return products;
            }

            return Array.Empty<WorkoutResponseDtoModel>();
        }

        public async Task<WorkoutResponseDtoModel[]> GetWorkoutsByUserIdAsync(string userId, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await _workoutApiClient.GetFromJsonAsync<WorkoutResponseDtoModel[]>($"/user/{userId}");

            if (products is not null)
            {
                return products;
            }

            return Array.Empty<WorkoutResponseDtoModel>();
        }

        public async Task<ApiResult> UpdateWorkoutAsync(WorkoutRequestDtoModel WorkoutToDelete, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.PostAsJsonAsync("", WorkoutToDelete);

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not update object" };
            }

            return new ApiResult { Success = true };
        }
    }
}
