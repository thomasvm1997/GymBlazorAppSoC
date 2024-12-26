using Pri.EindOpdracht.Core.Entities;
using Pri.EindOpdracht.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.EindOpdracht.Core.Services.Interfaces
{
    public interface IGoalService
    {
        Task<ResultModel<IEnumerable<Goal>>> ListAllAsync();
        Task<ResultModel<Goal>> GetByIdAsync(int goalId);
        Task<bool> DoesGoalIdExistsAsync(int goalId);
        Task<ResultModel<WorkoutType>> UpdateAsync(WorkoutType entity);
        Task<ResultModel<WorkoutType>> AddAsync(WorkoutType entity);
        Task<ResultModel<WorkoutType>> DeleteAsync(WorkoutType entity);
    }
}
