using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Services.Interfaces
{
    public interface IWorkoutService
    {
        Task<ResultModel<IEnumerable<Workout>>> ListAllAsync();
        Task<ResultModel<Workout>> GetByIdAsync(int workoutId);
        Task<bool> DoesWorkoutIdExistsAsync(int workoutId);
        Task<ResultModel<IEnumerable<Workout>>> GetWorkoutsByTypeAsync(int workoutTypeId);
        Task<ResultModel<IEnumerable<Workout>>> GetWorkoutsByUserAsync(int userId);
        Task<ResultModel<Workout>> UpdateAsync(Workout entity);
        Task<ResultModel<Workout>> AddAsync(Workout entity);
        Task<ResultModel<Workout>> DeleteAsync(Workout entity);
    }
}
