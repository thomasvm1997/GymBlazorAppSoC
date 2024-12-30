using Pri.EindOpdracht.ApiConsumer.Workouts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task CreateWorkoutAsync(WorkoutRequestDtoModel workoutToCreate, string token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkoutAsync(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutRequestDtoModel> GetWorkoutByIdAsync(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutRequestDtoModel[]> GetWorkoutsAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutRequestDtoModel[]> GetWorkoutsByUserIdAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkoutAsync(WorkoutRequestDtoModel WorkoutToDelete, string token)
        {
            throw new NotImplementedException();
        }
    }
}
