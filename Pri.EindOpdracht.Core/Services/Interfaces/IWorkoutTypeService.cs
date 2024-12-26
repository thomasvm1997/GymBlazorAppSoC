using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Services.Interfaces
{
    public interface IWorkoutTypeService
    {
        Task<ResultModel<IEnumerable<WorkoutType>>> ListAllAsync();
        Task<ResultModel<WorkoutType>> GetByIdAsync(int workoutTypeId);
        Task<bool> DoesWorkoutTypeIdExistsAsync(int workoutTypeId);
        Task<ResultModel<WorkoutType>> UpdateAsync(WorkoutType entity);
        Task<ResultModel<WorkoutType>> AddAsync(WorkoutType entity);
        Task<ResultModel<WorkoutType>> DeleteAsync(WorkoutType entity);
    }
}
