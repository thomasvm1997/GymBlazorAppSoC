using Pri.EindOpdracht.ApiConsumer.Goals.Models;
using Pri.EindOpdracht.ApiConsumer.Workouts.Models;
using Pri.EindOpdracht.ApiConsumer.WorkoutTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.WorkoutTypes
{
    public class WorkoutTypeApiService : IWorkoutTypeApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _workoutApiClient;
        public WorkoutTypeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _workoutApiClient = _httpClientFactory.CreateClient("WorkoutApiClient");
            _workoutApiClient.BaseAddress = new Uri(ApiRoutes.WorkoutTypes);
        }

        public async Task<ApiResult> CreateWorkoutTypeAsync(WorkoutTypeRequestDtoModel typeToCreate, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.PostAsJsonAsync("", typeToCreate);

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not create object" };
            }

            return new ApiResult { Success = true };
        }

        public async Task<WorkoutTypeResponseDtoModel> GetWorkoutTypeByIdAsync(int id, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var product = await _workoutApiClient.GetFromJsonAsync<WorkoutTypeResponseDtoModel>($"{id}");
            return product;
        }

        public async Task<WorkoutTypeResponseDtoModel[]> GetWorkoutTypesAsync(string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await _workoutApiClient.GetFromJsonAsync<WorkoutTypeResponseDtoModel[]>("");

            if (products is not null)
            {
                return products;
            }

            return Array.Empty<WorkoutTypeResponseDtoModel>();
        }

        public async Task<WorkoutTypeResponseDtoModel[]> GetWorkoutTypesByUserIdAsync(string userId, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await _workoutApiClient.GetFromJsonAsync<WorkoutTypeResponseDtoModel[]>($"/user/{userId}");

            if (products is not null)
            {
                return products;
            }

            return Array.Empty<WorkoutTypeResponseDtoModel>();
        }

        public async Task<ApiResult> UpdateWorkoutTypeAsync(WorkoutTypeRequestDtoModel typeToUpdate, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.PostAsJsonAsync("", typeToUpdate);

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not update object" };
            }

            return new ApiResult { Success = true };
        }
    }
}
