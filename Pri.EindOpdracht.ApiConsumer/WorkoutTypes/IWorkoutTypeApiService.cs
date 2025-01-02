using Pri.EindOpdracht.ApiConsumer.Workouts.Models;
using Pri.EindOpdracht.ApiConsumer.WorkoutTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.ApiConsumer.WorkoutTypes
{
    public interface IWorkoutTypeApiService
    {
        Task<WorkoutTypeResponseDtoModel[]> GetWorkoutTypesAsync(string token);
        Task<WorkoutTypeResponseDtoModel> GetWorkoutTypeByIdAsync(int id, string token);
        Task<WorkoutTypeResponseDtoModel[]> GetWorkoutTypesByUserIdAsync(string userId, string token);
        Task<ApiResult> CreateWorkoutTypeAsync(WorkoutTypeRequestDtoModel typeToCreate, string token);
        Task<ApiResult> UpdateWorkoutTypeAsync(WorkoutTypeRequestDtoModel typeToUpdate, string token);
        Task<ApiResult> DeleteWorkoutTypeAsync(int id, string token);
    }
}
