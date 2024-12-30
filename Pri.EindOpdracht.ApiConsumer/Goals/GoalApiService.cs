using Pri.EindOpdracht.ApiConsumer.Goals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _workoutApiClient.BaseAddress = new Uri(ApiRoutes.Auth);
        }
        public Task CreateGoalAsync(GoalRequestDtoModel goalToCreate, string token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGoalAsync(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<GoalResponseDtoModel> GetGoalByIdAsync(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<GoalResponseDtoModel[]> GetGoalsAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<GoalResponseDtoModel[]> GetGoalsByUserIdAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGoalAsync(GoalRequestDtoModel goalToUpdate, string token)
        {
            throw new NotImplementedException();
        }
    }
}
