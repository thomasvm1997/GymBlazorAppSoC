using Pri.EindOpdracht.ApiConsumer.WorkoutTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task CreateWorkoutTypeAsync(WorkoutTypeRequestDtoModel typeToCreate, string token)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutTypeResponseDtoModel> GetWorkoutTypeByIdAsync(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutTypeResponseDtoModel[]> GetWorkoutTypesAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<WorkoutTypeResponseDtoModel[]> GetWorkoutTypesByUserIdAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkoutTypeAsync(WorkoutTypeRequestDtoModel typeToUpdate, string token)
        {
            throw new NotImplementedException();
        }
    }
}
