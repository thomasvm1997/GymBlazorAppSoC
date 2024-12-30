using Pri.EindOpdracht.ApiConsumer.Goals.Models;
using Pri.EindOpdracht.ApiConsumer.Workouts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.Workouts
{
    public interface IWorkoutApiService
    {
        Task<WorkoutResponseDtoModel[]> GetWorkoutsAsync(string token);
        Task<WorkoutResponseDtoModel> GetWorkoutByIdAsync(int id, string token);
        Task<WorkoutResponseDtoModel[]> GetWorkoutsByUserIdAsync(string userId, string token);
        Task<ApiResult> CreateWorkoutAsync(WorkoutRequestDtoModel workoutToCreate, string token);
        Task<ApiResult> UpdateWorkoutAsync(WorkoutRequestDtoModel WorkoutToDelete, string token);
        Task<ApiResult> DeleteWorkoutAsync(int id, string token);
    }
}
