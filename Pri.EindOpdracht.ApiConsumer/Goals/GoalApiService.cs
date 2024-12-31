using Azure;
using Pri.EindOpdracht.ApiConsumer.Goals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.Goals
{
    public class GoalApiService : IGoalApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _workoutApiClient;
        public GoalApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _workoutApiClient = _httpClientFactory.CreateClient("WorkoutApiClient");
            _workoutApiClient.BaseAddress = new Uri(ApiRoutes.Goals);
        }
        public async Task<ApiResult> CreateGoalAsync(GoalRequestDtoModel goalToCreate, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.PostAsJsonAsync("", goalToCreate);

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not create object" };
            }

            return new ApiResult { Success = true };
        }

        public async Task<ApiResult> DeleteGoalAsync(int id, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.DeleteAsync($"{id}");

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not delete object" };
            }

            return new ApiResult { Success = true };
        }

        public async Task<GoalResponseDtoModel> GetGoalByIdAsync(int id, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var product = await _workoutApiClient.GetFromJsonAsync<GoalResponseDtoModel>($"{id}");
            return product;
        }

        public async Task<GoalResponseDtoModel[]> GetGoalsAsync(string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await _workoutApiClient.GetFromJsonAsync<GoalResponseDtoModel[]>("");

            if (products is not null)
            {
                return products;
            }

            return Array.Empty<GoalResponseDtoModel>();
        }

        public async Task<GoalResponseDtoModel[]> GetGoalsByUserIdAsync(string userId, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var products = await _workoutApiClient.GetFromJsonAsync<GoalResponseDtoModel[]>($"user/{userId}");

            if (products is not null)
            {
                return products;
            }

            return Array.Empty<GoalResponseDtoModel>();
        }

        public async Task<ApiResult> UpdateGoalAsync(GoalRequestDtoModel goalToUpdate, string token)
        {
            _workoutApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _workoutApiClient.PutAsJsonAsync("", goalToUpdate);

            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult { Success = false, ErrorMessage = "Could not update object" };
            }

            return new ApiResult { Success = true };
        }
    }
}
