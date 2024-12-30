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
        Task<WorkoutRequestDtoModel[]> GetWorkoutsAsync(string token);
        Task<WorkoutRequestDtoModel> GetWorkoutByIdAsync(int id, string token);
        Task<WorkoutRequestDtoModel[]> GetWorkoutsByUserIdAsync(string userId, string token);
        Task CreateWorkoutAsync(WorkoutRequestDtoModel workoutToCreate, string token);
        Task UpdateWorkoutAsync(WorkoutRequestDtoModel WorkoutToDelete, string token);
        Task DeleteWorkoutAsync(int id, string token);
    }
}
